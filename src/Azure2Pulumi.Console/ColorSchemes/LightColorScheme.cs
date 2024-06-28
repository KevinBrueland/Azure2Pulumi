using Spectre.Console;

namespace Azure2Pulumi.Console.ColorSchemes
{
    internal class LightColorScheme : ColorScheme
    {
        internal override string SchemeName => "light";

        public override Color Info => Color.Black;

        public override Color Warning => Color.Orange1;

        public override Color Error => Color.Red3_1;

        public override Color Success => Color.DarkGreen;

        public override Color Status => Color.Maroon;

        public override Color MoreChoiceText => Color.Grey15;

        public override Color SelectedHighlightText => Color.Maroon;

        public override Color PromptTitle => Color.Black;

        public override Color ValidationErrorText => Color.Red3_1;

        public override Color InstructionSelect => Color.DarkBlue;

        public override Color InstructionAccept => Color.DarkGreen;

        public override Color Logo => Color.Black;

        
    }
}
