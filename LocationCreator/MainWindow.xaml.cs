using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

using game;
using System.Globalization;
using System.Resources;
using System;
using LocationCreator.Properties.Lang;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LocationCreator {
    public record RoomVisual : Room {
        public required Rectangle View { get; set; }
        public required Point Position { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Point contextMenuPos;
        private readonly ObservableCollection<RoomVisual> rooms = new();

        private void UpdateCanvas(object? sender, NotifyCollectionChangedEventArgs e) {
            MainCanvas.Children.Clear();
            foreach (RoomVisual room in rooms) {
                MainCanvas.Children.Add(room.View);
                room.View.SetValue(Canvas.TopProperty, room.Position.Y);
                room.View.SetValue(Canvas.LeftProperty, room.Position.X);
            }
            //switch (e.Action) {
            //    case NotifyCollectionChangedAction.Add:
            //        if (e.NewItems is not null) {
            //            foreach (RoomVisual room in e.NewItems) {
            //                Canvas.SetTop(room.View, room.Position.Y);
            //                Canvas.SetLeft(room.View, room.Position.X);
            //            }
            //        }
            //        break;
            //    case NotifyCollectionChangedAction.Remove:
            //        break;
            //}
        }

        private static List<CultureInfo> GetAvailableCultures() {
            ResourceManager resourceManager = new ResourceManager(typeof(Lang));

            List<CultureInfo> resourceSets = new();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures)) {
                ResourceSet? resourceSet = resourceManager.GetResourceSet(culture, true, false);
                if (resourceSet is not null) {
                    resourceSets.Add(culture);
                }
            }

            return resourceSets;
        }

        private void InitializeCultureSettings() {
            foreach (var culture in GetAvailableCultures()) {
                MenuItem menuItem = new() {
                    Header = culture.Name
                };
                menuItem.Click += (_, _) => MessageBox.Show(culture.Name);//System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture.Name);
                StatusBar_File_Settings_Languages.Items.Add(culture);
            }
        }

        public MainWindow() {
            InitializeComponent();
            InitializeCultureSettings();
            rooms.CollectionChanged += UpdateCanvas;
        }

        private void AddRoom(Point position) {
            (double Width, double Height) size = (50, 50);

            Rectangle rectangle = new() {
                Width = size.Width,
                Height = size.Height,
                Fill = Brushes.Fuchsia
            };

            rooms.Add(new RoomVisual() { Name = "Room", View = rectangle, Position = position});
        }

        private void MainCanvas_ContextMenu_AddRoom_Click(object sender, RoutedEventArgs e) {
            Point position = new Point(contextMenuPos.X - 25, contextMenuPos.Y - 25);
            AddRoom(position);
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2) {
                Point clickPosition = e.GetPosition(MainCanvas);
                Point position = new Point(clickPosition.X - 25, clickPosition.Y - 25);
                AddRoom(position);
            }
        }

        private void MainCanvas_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
            contextMenuPos = Mouse.GetPosition(MainCanvas);
        }

        private void ToJson() {
            var json =
                from room in rooms
                select new Dictionary<string, object>() {
                    { "X", room.Position.X },
                    { "Y", room.Position.Y },
                };
            using StreamWriter writer = new("rectangles.json");
            writer.Write(JsonConvert.SerializeObject(json));
        }

        private void FromJson() {
            using StreamReader reader = new("rectangles.json");
            var json = reader.ReadToEnd();
            var rects = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
            if (rects is null) {
                return;
            }
            rooms.Clear();
            foreach (var rectangle_dict in rects) {
                AddRoom(new Point((double)rectangle_dict["X"], (double)rectangle_dict["Y"]));
            }
        }

        private void StatusBar_File_New_Executed(object sender, ExecutedRoutedEventArgs e) {
            rooms.Clear();
        }

        private void StatusBar_File_Open_Executed(object sender, RoutedEventArgs e) {
            FromJson();
        }

        private void StatusBar_File_Save_Executed(object sender, RoutedEventArgs e) {
            ToJson();
        }

        private void StatusBar_File_New_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void StatusBar_File_Open_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void StatusBar_File_Save_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
    }
}
