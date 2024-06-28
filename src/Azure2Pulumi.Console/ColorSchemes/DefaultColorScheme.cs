using Spectre.Console;

namespace Azure2Pulumi.Console.ColorSchemes
{
    internal class DefaultColorScheme : ColorScheme
    {
        internal override string SchemeName => "default";

        public override Color Info => Color.White;

        public override Color Warning => Color.Yellow3_1;

        public override Color Error => Color.Red3_1;

        public override Color Success => Color.Chartreuse3_1;

        public override Color Status => Color.SteelBlue1;

        public override Color MoreChoiceText => Color.Grey85;

        public override Color SelectedHighlightText => Color.Cyan1;

        public override Color PromptTitle => Color.Yellow;

        public override Color ValidationErrorText => Color.Red3_1;

        public override Color InstructionSelect => Color.DeepSkyBlue1;

        public override Color InstructionAccept => Color.SpringGreen2_1;

        public override Color Logo => Color.White;

        
    }
}
