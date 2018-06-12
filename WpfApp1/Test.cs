namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using ModernControls.Controls;
    using ModernControls.Controls.DynamicUi;
    using ModernControls.Controls.DynamicUI;

    public class Test
    {
        public enum Tester
        {
            One,

            Two
        }

        [Flags]
        public enum Flagger
        {
            One = 1,

            Two = 2,

            Four = 4,

            Five = 8,

            Six = 0x10,

            Seven = 0x20,

            Eight = 0x40,

            Nine = 0x80
        }

        public Test()
        {
            this.TestValue2 = 1000;
        }

        [FileBrowse(true)]
        public string Output { get; set; }

        [FileBrowse(false)]
        public string Input { get; set; }

        public string OutputData { get; set; }

        public double TestValue2 { get; }

        public double Hello { get; set; }

        [SizeOption(MinimumHeight = -100, MinimumWidth = -100, MaximumHeight = 100, MaximumWidth = 100, Step = .5)]
        public Size Size { get; set; }

        public Flagger ttt { get; set; }

        public List<string> Tessssst { get; set; }
    }
}