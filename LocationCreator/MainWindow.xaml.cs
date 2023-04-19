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

namespace LocationCreator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Point contextMenuPos;
        private readonly List<Rectangle> rectangles = new();

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
        }

        private void AddRoom(Point position) {
            (double Width, double Height) size = (50, 50);

            Rectangle rectangle = new() {
                Width = size.Width,
                Height = size.Height,
                Fill = Brushes.Fuchsia
            };

            MainCanvas.Children.Add(rectangle);
            Canvas.SetTop(rectangle, position.Y - (size.Width/2));
            Canvas.SetLeft(rectangle, position.X - (size.Width/2));
            rectangles.Add(rectangle);
        }

        private void MainCanvas_ContextMenu_AddRoom_Click(object sender, RoutedEventArgs e) {
            AddRoom(contextMenuPos);
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2) {
                AddRoom(e.GetPosition(MainCanvas));
            }
        }

        private void MainCanvas_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
            contextMenuPos = Mouse.GetPosition(MainCanvas);
        }

        private void ToJson() {
            var json =
                from rectangle in rectangles
                select new Dictionary<string, object>() {
                    { "X", Canvas.GetLeft(rectangle) },
                    { "Y", Canvas.GetTop(rectangle) },
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
            rectangles.Clear();
            foreach (var rectangle_dict in rects) {
                AddRoom(new Point((double)rectangle_dict["X"], (double)rectangle_dict["Y"]));
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control) {
                switch (e.Key) {
                    case Key.S: {
                        ToJson();
                        break;
                    }
                    case Key.O: {
                        FromJson();
                        break;
                    }
                }
            }
        }

        private void StatusBar_File_New_Executed(object sender, ExecutedRoutedEventArgs e) {
            rectangles.Clear();
            MainCanvas.Children.Clear();
        }

        private void StatusBar_File_Open_Executed(object sender, RoutedEventArgs e) {
            FromJson();
        }

        private void StatusBar_File_Save_Executed(object sender, RoutedEventArgs e) {
            ToJson();
        }
    }
}
