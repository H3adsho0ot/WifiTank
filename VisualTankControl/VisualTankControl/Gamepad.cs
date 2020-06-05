using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace VisualTankControl
{
    public class Gamepad
    {
        public delegate void GamepadInput(JoystickUpdate update);
        public event GamepadInput RaiseGamepadInput;

        private Joystick _joystick;
        private int deadband = 2500;

        public Gamepad(Guid guid)
        {
            DirectInput directInput = new DirectInput();
            _joystick = new Joystick(directInput, guid);
            _joystick.Properties.BufferSize = 256;
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
                _joystick.Poll();
                var datas = _joystick.GetBufferedData();
                foreach (JoystickUpdate update in datas)
                {
                    RaiseGamepadInput(MapValue(update));
                }
            }
        }

        private JoystickUpdate MapValue(JoystickUpdate input)
        {
            if (input.Offset == JoystickOffset.Y)
            {
                input.Value = remap(input.Value, 0, 65535, 100, -100);
            }
            if (input.Offset == JoystickOffset.RotationZ)
            {
                input.Value = remap(input.Value, 0, 65535, 100, -100);
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
