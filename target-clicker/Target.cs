using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace TargetClicker
{
    class Target
    {
        public enum MouseActionType
        {
            Click       = 0,
            DoubleClick = 1,
            Down        = 2,
            Up          = 3,
        }

        static KeyConverter keyConverter = null;
        static WindowsInput.InputSimulator inputSimulator = null;

        private string strShortCutKeys, strMouseEvent, strCordinate;

        static Target()
        {
            if(Target.keyConverter == null)
            {
                Target.keyConverter = new KeyConverter();
            }
            if(Target.inputSimulator == null)
            {
                Target.inputSimulator = new WindowsInput.InputSimulator();
            }


        }

        public string StrName { get; set; }
        public string StrShortCutKeys
        {
            get
            {
                return this.strShortCutKeys;
            }
            set {
                this.strShortCutKeys = value;
                setKeyFromString(strShortCutKeys);
                setModifierKeyFromString(strShortCutKeys);
            }
        }
        public string StrMouseEvent
        {
            get
            {
                return this.strMouseEvent;
            }
            set
            {
                this.strMouseEvent = value;
                setMouseEventFromString(this.strMouseEvent);
            }
        }
        public string StrCordinate
        {
            get
            {
                return this.strCordinate;
            }
            set
            {
                this.strCordinate = value;
                string[] strCordinates = this.strCordinate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                this.CordinateX = Double.Parse(strCordinates[0]);
                this.CordinateY = Double.Parse(strCordinates[1]);
                //TODO Error
            }
        }

        public Key Key { get; private set; }
        public ModifierKeys ModifierKeys { get; private set; }
        public string MouseEvent { get; private set; }
        public MouseButton MouseButton { get; private set; }
        public MouseActionType MouseAction { get; private set; }
        public double CordinateX { get; private set; }
        public double CordinateY { get; private set; }

        public void RegistHotKey()
        {
            HotkeyManager.Current.AddOrReplace(this.StrName, this.Key, this.ModifierKeys, this.runAction);
        }

        private void runAction(object sender, HotkeyEventArgs e)
        {
            var sim = Target.inputSimulator;

            sim.Mouse.MoveMouseTo(this.CordinateX, this.CordinateY);

            switch (this.MouseAction)
            {
                case Target.MouseActionType.Click:
                    doButtonClick(this.MouseButton);
                    break;
                case Target.MouseActionType.DoubleClick:
                    doButtonDoubleClick(this.MouseButton);
                    break;
                case Target.MouseActionType.Down:
                    doButtonDown(this.MouseButton);
                    break;
                case Target.MouseActionType.Up:
                    doButtonUp(this.MouseButton);
                    break;
            }
        }

        private void doButtonClick(MouseButton btn)
        {
            var sim = Target.inputSimulator;

            switch (btn)
            {
                case MouseButton.Left:
                    sim.Mouse.LeftButtonClick();
                    break;
                case MouseButton.Right:
                    sim.Mouse.RightButtonClick();
                    break;
                default:
                    break;

            }
        }

        private void doButtonDoubleClick(MouseButton btn)
        {
            var sim = Target.inputSimulator;

            switch (btn)
            {
                case MouseButton.Left:
                    sim.Mouse.LeftButtonDoubleClick();
                    break;
                case MouseButton.Right:
                    sim.Mouse.RightButtonDoubleClick();
                    break;
                default:
                    break;

            }
        }

        private void doButtonDown(MouseButton btn)
        {
            var sim = Target.inputSimulator;

            switch (btn)
            {
                case MouseButton.Left:
                    sim.Mouse.LeftButtonDown();
                    break;
                case MouseButton.Right:
                    sim.Mouse.RightButtonDown();
                    break;
                default:
                    break;

            }
        }

        private void doButtonUp(MouseButton btn)
        {
            var sim = Target.inputSimulator;

            switch (btn)
            {
                case MouseButton.Left:
                    sim.Mouse.LeftButtonUp();
                    break;
                case MouseButton.Right:
                    sim.Mouse.RightButtonUp();
                    break;
                default:
                    break;

            }
        }



        private void setKeyFromString(string strKeys)
        {
            KeyConverter k = Target.keyConverter;
            this.Key = (Key) k.ConvertFromString(strKeys.Substring(strKeys.LastIndexOf("+")+1));
            // TODO: Error process

        }

        private void setModifierKeyFromString(string strKeys)
        {
            var str = strKeys.ToUpper();

            this.ModifierKeys = 0x0;
            if (str.Contains("CTRL"))    { this.ModifierKeys |= System.Windows.Input.ModifierKeys.Control; }
            if (str.Contains("ALT"))     { this.ModifierKeys |= System.Windows.Input.ModifierKeys.Alt; }
            if (str.Contains("SHIFT"))   { this.ModifierKeys |= System.Windows.Input.ModifierKeys.Shift; }
            if (str.Contains("WINDOWS")) { this.ModifierKeys |= System.Windows.Input.ModifierKeys.Windows; }
        }

        private void setMouseEventFromString(string strMouseEvent)
        {
            var str = strMouseEvent.ToUpper();

            if (str.Contains("LEFT"))   { this.MouseButton = MouseButton.Left; }
            if (str.Contains("RIGHT"))  { this.MouseButton = MouseButton.Right; }
            if (str.Contains("MIDDLE")) { this.MouseButton = MouseButton.Middle; }

            if (str.Contains("CLICK"))       { this.MouseAction = MouseActionType.Click; }
            if (str.Contains("DOUBLECLICK")) { this.MouseAction = MouseActionType.DoubleClick; }
            if (str.Contains("DOWN"))        { this.MouseAction = MouseActionType.Down; }
            if (str.Contains("UP"))          { this.MouseAction = MouseActionType.Up; }
        }
    }
}
