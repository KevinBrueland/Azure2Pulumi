using Spectre.Console;

namespace Azure2Pulumi.Console.ColorSchemes
{
    public abstract class ColorScheme
    {
        internal abstract string SchemeName { get; }
        public abstract Color Info { get; }
        public abstract Color Warning { get; }
        public abstract Color Error { get; }
        public abstract Color Success { get; }
        public abstract Color Status { get; }
        public abstract Color MoreChoiceText { get; }
        public abstract Color SelectedHighlightText { get; }
        public abstract Color PromptTitle { get; }
        public abstract Color ValidationErrorText { get; }

        public abstract Color InstructionSelect { get; }
        public abstract Color InstructionAccept { get; }
        public abstract Color Logo { get; }

    }
}
