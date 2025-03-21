// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable SA1121 // UseBuiltInTypeAlias

using System;
using CommunityToolkit.Notifications.Adaptive.Elements;
using BindableString = CommunityToolkit.Notifications.BindableString;

namespace CommunityToolkit.Notifications;

/// <summary>
/// An adaptive text element.
/// </summary>
public sealed class AdaptiveText
    : IAdaptiveChild,
    IAdaptiveSubgroupChild,
    ITileBindingContentAdaptiveChild,
    IToastBindingGenericChild
{
    /// <summary>
    /// Gets or sets the text to display. Data binding support added in Creators Update,
    /// only works for toast top-level text elements.
    /// </summary>
    public BindableString Text { get; set; }

    /// <summary>
    /// Gets or sets the target locale of the XML payload, specified as a BCP-47 language tags
    /// such as "en-US" or "fr-FR". The locale specified here overrides any other specified
    /// locale, such as that in binding or visual. If this value is a literal string,
    /// this attribute defaults to the user's UI language. If this value is a string reference,
    /// this attribute defaults to the locale chosen by Windows Runtime in resolving the string.
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets the style that controls the text's font size, weight, and opacity.
    /// Note that for Toast, the style will only take effect if the text is inside an <see cref="AdaptiveSubgroup"/>.
    /// </summary>
    public AdaptiveTextStyle HintStyle { get; set; }

    /// <summary>
    /// Gets or sets a value whether text wrapping is enabled. For Tiles, this is false by default.
    /// For Toasts, this is true on top-level text elements, and false inside an <see cref="AdaptiveSubgroup"/>.
    /// Note that for Toast, setting wrap will only take effect if the text is inside an
    /// <see cref="AdaptiveSubgroup"/> (you can use HintMaxLines = 1 to prevent top-level text elements from wrapping).
    /// </summary>
    public bool? HintWrap { get; set; }

    private int? _hintMaxLines;

    /// <summary>
    /// Gets or sets the maximum number of lines the text element is allowed to display.
    /// For Tiles, this is infinity by default. For Toasts, top-level text elements will
    /// have varying max line amounts (and in the Anniversary Update you can change the max lines).
    /// Text on a Toast inside an <see cref="AdaptiveSubgroup"/> will behave identically to Tiles (default to infinity).
    /// </summary>
    public int? HintMaxLines
    {
        get
        {
            return _hintMaxLines;
        }

        set
        {
            if (value != null)
            {
                Element_AdaptiveText.CheckMaxLinesValue(value.Value);
            }

            _hintMaxLines = value;
        }
    }

    private int? _hintMinLines;

    /// <summary>
    /// Gets or sets the minimum number of lines the text element must display.
    /// Note that for Toast, this property will only take effect if the text is inside an <see cref="AdaptiveSubgroup"/>.
    /// </summary>
    public int? HintMinLines
    {
        get
        {
            return _hintMinLines;
        }

        set
        {
            if (value != null)
            {
                Element_AdaptiveText.CheckMinLinesValue(value.Value);
            }

            _hintMinLines = value;
        }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of the text. Note that for Toast, this property will
    /// only take effect if the text is inside an <see cref="AdaptiveSubgroup"/>.
    /// </summary>
    public AdaptiveTextAlign HintAlign { get; set; }

    internal Element_AdaptiveText ConvertToElement()
    {
        var answer = new Element_AdaptiveText()
        {
            Lang = Language,
            Style = HintStyle,
            Wrap = HintWrap,
            MaxLines = HintMaxLines,
            MinLines = HintMinLines,
            Align = HintAlign
        };

        answer.Text = Text?.ToXmlString();

        return answer;
    }

    /// <summary>
    /// Returns the value of the Text property.
    /// </summary>
    /// <returns>The value of the Text property.</returns>
    public override string ToString()
    {
        return Text;
    }
}
