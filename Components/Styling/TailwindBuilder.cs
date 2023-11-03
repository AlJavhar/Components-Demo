using Components.Enums;
using Components.Infrastructure;

namespace Components.Styling;

internal class TailwindBuilder
{
    private readonly Dictionary<int, string> _map = new()
    {
        { 0,"50" },
        { 1, "100" },
        { 2, "200" },
        { 3, "300" },
        { 4, "400" },
        { 5, "500" },
        { 6, "600" },
        { 7, "700" },
        { 8, "800" },
        { 9, "900" },
        { 10, "950" }
    };

    private string css = string.Empty;
    private string Classes { get; set; } = string.Empty;
    private ColorWithShade? BackgroundColor { get; set; }
    private ColorWithShade? BorderColor { get; set; }
    private ColorWithShade? HoverBackgroundColor { get; set; }
    private double? Padding { get; set; }
    private double? VerticalPadding { get; set; }
    private double? HorizontalPadding { get; set; }
    private TextTransform TextTransform { get; set; }
    private MkSize FontSize { get; set; }
    private ColorWithShade? FontColor { get; set; }

    public static TailwindBuilder New()
    {
        return new TailwindBuilder();
    }

    public string Build()
    {
        if (BackgroundColor != null)
        {
            css = Append($"bg-{Translate(BackgroundColor)}");
            Console.WriteLine(css);
        }

        if (HoverBackgroundColor != null)
        {
            css = Append($"hover:bg-{Translate(HoverBackgroundColor)}");
            Console.WriteLine(css);
        }

        if (HoverBackgroundColor == null && BackgroundColor != null && BackgroundColor.Shade < 10)
        {
            var hover = new ColorWithShade(BackgroundColor.BaseColor, BackgroundColor.Shade + 1);
            css = Append($"hover:bg-{Translate(hover)}");
            Console.WriteLine(css);
        }

        if (HoverBackgroundColor == null && BackgroundColor == null && BorderColor != null)
        {
            var hover = new ColorWithShade(BorderColor.BaseColor, 0);
            css = Append($"hover:bg-{Translate(hover)}");
            Console.WriteLine(css);
        }

        if (Padding != null)
        {
            css = Append($"p-{Padding}");
            Console.WriteLine(css);
        }

        if (VerticalPadding != null)
        {
            css = Append($"py-{VerticalPadding}");
            Console.WriteLine(css);
        }

        if (HorizontalPadding != null)
        {
            css = Append($"px-{HorizontalPadding}");
            Console.WriteLine(css);
        }

        if (TextTransform != TextTransform.Default)
        {
            css = Append(TransformText(TextTransform));
            Console.WriteLine(css);
        }

        if (!string.IsNullOrWhiteSpace(Classes))
        {
            css = Append(Classes);
            Console.WriteLine(css);
        }

        if (BorderColor != null)
        {
            css = Append($"border border-{Translate(BorderColor)}");
            Console.WriteLine(css);
        }

        if (FontColor != null)
        {
            css = Append($"text-{Translate(FontColor)}");
            Console.WriteLine(css);
        }

        css = Append(ToTextSize(FontSize));

        return css.Trim();  //Этот код удаляет все начальные и конечные пробельные символы из строки 
    }

    public TailwindBuilder AddBackground(ColorWithShade color)
    {
        BackgroundColor = color;

        return this;
    }

    public TailwindBuilder AddBackgroundHover(ColorWithShade color)
    {
        HoverBackgroundColor = color;

        return this;
    }

    public TailwindBuilder AddBorder(ColorWithShade color)
    {
        BorderColor = color;

        return this;
    }
    public TailwindBuilder SetFontColor(ColorWithShade color)
    {
        FontColor = color;
        return this;
    }
    public TailwindBuilder AddPadding(double padding)
    {
        Padding = padding;

        return this;
    }

    public TailwindBuilder AddPadding(double x, double y)
    {
        VerticalPadding = y;
        HorizontalPadding = x;
        return this;
    }

    public TailwindBuilder AddVerticalPadding(double padding)
    {
        VerticalPadding = padding;

        return this;
    }

    public TailwindBuilder AddHorizontalPadding(double padding)
    {
        HorizontalPadding = padding;

        return this;
    }

    public TailwindBuilder AddClasses(string classes)
    {
        Classes = $"{Classes} {classes}";

        return this;
    }

    public TailwindBuilder AddTextTransform(TextTransform textTransform)
    {
        TextTransform = textTransform;

        return this;
    }

    public TailwindBuilder SetFontSize(MkSize fontSize)
    {
        FontSize = fontSize;

        return this;
    }

    private string Translate(ColorWithShade color)
    {
        return $"{color.BaseColor}-{_map[color.Shade]}";
    }

    private string Append(string text)
    {
        css = $"{css} {text}";

        return css;
    }

    private string TransformText(TextTransform textTransform)
    {
        return textTransform switch
        {
            TextTransform.Uppercase => "uppercase",
            TextTransform.Lowercase => "lowercase",
            TextTransform.Capitalize => "capitalize",
            _ => string.Empty,
        };
    }

    private string ToTextSize(MkSize fontSize)
    {
        return fontSize switch
        {
            MkSize.Small => "text-sm",
            MkSize.Medium => "text-base",
            MkSize.Large => "text-lg",
            _ => "text-base"
        };
    }
}
