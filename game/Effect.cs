namespace game {

    public abstract class BaseEffect {

        public virtual void OnStart() {
        }

        public virtual void OnTurn() {
        }

        public virtual void OnEnd() {
        }
    }

    public class SummonGoldenSaint : BaseEffect {

        public override void OnStart() {
            // Add golden saint to party
        }

        public override void OnEnd() {
            // Remove golden saint from party
        }
    }

    //public abstract class BaseEffect {
    //    public Action<Character> Act { get; init; }
    //    public int Cost { get; }

    //    protected BaseEffect(Action<Character> act) {
    //        Act = act;
    //    }
    //}

    //public class SelfEffect : BaseEffect {
    //    public SelfEffect(Action<Character> act) : base(act) {
    //    }
    //}

    //public class TouchEffect : BaseEffect {
    //    public TouchEffect(Action<Character> act) : base(act) {
    //    }
    //}

    //public class DistantEffect : BaseEffect {
    //    public DistantEffect(Action<Character> act) : base(act) {
    //    }
    //}
}