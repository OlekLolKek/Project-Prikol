using System.Collections.Generic;


namespace ProjectPrikol
{
    public class Controllers : IInitializable, IExecutable, ILateExecutable, ICleanable
    {
        private readonly List<IInitializable> _initializables;
        private readonly List<IExecutable> _executables;
        private readonly List<ILateExecutable> _lateExecutables;
        private readonly List<ICleanable> _cleanables;
        
        
        public Controllers()
        {
            _initializables = new List<IInitializable>();
            _executables = new List<IExecutable>();
            _lateExecutables = new List<ILateExecutable>();
            _cleanables = new List<ICleanable>();
        }

        internal Controllers Add(IControllable controller)
        {
            if (controller is IInitializable init)
                _initializables.Add(init);

            if (controller is IExecutable execute)
                _executables.Add(execute);

            if (controller is ILateExecutable lateExecutable)
                _lateExecutables.Add(lateExecutable);

            if (controller is ICleanable cleanup)
                _cleanables.Add(cleanup);

            return this;
        }

        public void Initialize()
        {
            foreach (var controller in _initializables)
                controller.Initialize();
        }

        public void Execute(float deltaTime)
        {
            foreach (var controller in _executables)
                controller.Execute(deltaTime);
        }

        public void LateExecute(float deltaTime)
        {
            foreach (var controller in _lateExecutables)
                controller.LateExecute(deltaTime);
        }

        public void Cleanup()
        {
            foreach (var controller in _cleanables)
                controller.Cleanup();
        }
    }
}