using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace VisualTankControl
{
    public class Gamepad
    {
        public delegate void GamepadInput(JoystickUpdate update);
        public event GamepadInput RaiseGamepadInput;

        private Joystick _joystick;
        private int _deadband = 5000;
        private int _center = 32767;
        private int _refreshDelay = 100;

        public Gamepad(Guid guid)
        {
            DirectInput directInput = new DirectInput();
            _joystick = new Joystick(directInput, guid);
            _joystick.Properties.BufferSize = 128;
            _joystick.Acquire();
        }

        public List<EffectInfo> Effects
        {
            get
            {
                if (_joystick != null)
                {
                    return _joystick.GetEffects().ToList();
                }
                return null;
            }
        }

        public void StartPoll()
        {
            Task.Run(Poll);
        }

        private async void Poll()
        {
            while (true)
            {
                //_joystick.Poll();
                //JoystickUpdate[] updates = _joystick.GetBufferedData();
                //foreach(JoystickUpdate update in updates)
                //{
                //    RaiseGamepadInput(MapValue(update));                    
                //}
                var joystickState = new JoystickState();
                _joystick.GetCurrentState(ref joystickState);
                //Console.WriteLine(joystickState);
                //RaiseGamepadInput(MapValue(new JoystickUpdate() { RawOffset = (int)JoystickOffset., Value = joystickState.Z }));
                RaiseGamepadInput(MapValue(new JoystickUpdate() { RawOffset = (int)JoystickOffset.Z, Value = joystickState.Z }));
                RaiseGamepadInput(MapValue(new JoystickUpdate() { RawOffset = (int)JoystickOffset.RotationZ, Value = joystickState.RotationZ }));
                RaiseGamepadInput(MapValue(new JoystickUpdate() { RawOffset = (int)JoystickOffset.Y, Value = joystickState.Y }));
                RaiseGamepadInput(MapValue(new JoystickUpdate() { RawOffset = (int)JoystickOffset.X, Value = joystickState.X }));

                Thread.Sleep(_refreshDelay);

                //if (updates.Count() > 0)
                //{
                //    JoystickUpdate update = updates.Last();
                //    RaiseGamepadInput(MapValue(update));
                //    //Console.WriteLine(update);
                //}
            }
        }

        private JoystickUpdate MapValue(JoystickUpdate input)
        {
            if (input.Offset == JoystickOffset.Y || input.Offset == JoystickOffset.RotationZ || input.Offset == JoystickOffset.Z || input.Offset == JoystickOffset.X)
            {
                if (input.Value < _center + _deadband && input.Value > _center - _deadband)
                {
                    input.Value = 0;
                }
                else
                {
                    input.Value = remap(input.Value, 0, 65535, 100, -100);
                }
            }

            return input;
        }

        private int remap(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return (int)to;
        }
    }
}
