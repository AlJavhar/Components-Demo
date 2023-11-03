using Components.Enums;
using Components.Infrastructure;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace Components;

public interface IButtonProps
{
    public MkSize Size { get; set; }
    public bool Loading { get; set; }
    public string Styles { get; set; }
    public MkSize Radius { get; set; }
    public bool Uppercase { get; set; }
    public ColorWithShade Color { get; set; }
    public ButtonVariant Variant { get; set; }
    public RenderFragment? LeftIcon { get; set; }
    public RenderFragment? RightIcon { get; set; }
    public RenderFragment? ChildContent { get; set; }
}