using UnityEngine;
using System.Collections.Generic;

public class PaintColorMap {
	public static Dictionary<PaintColor, List<PaintColor>> paintLookup;
	public static bool isPopulated = false;

	public static bool doColorsConnect(PaintColor color1, PaintColor color2) {
		return doesFirstColorComposeSecond (color1, color2) || doesFirstColorComposeSecond(color2, color1);
	}

	private static bool doesFirstColorComposeSecond(PaintColor color1, PaintColor color2) {
		bool isComposed = false;

		if (!isPopulated) {
			populateColorMap();
		}

		if (paintLookup.ContainsKey (color2)) {
			 isComposed = paintLookup[color2].Contains(color1);
		}

		return isComposed;
	}

	private static void populateColorMap() {
		paintLookup = new Dictionary<PaintColor, List<PaintColor>>();

		// primary colors
		List<PaintColor> primary = new List<PaintColor> ();

		paintLookup.Add (PaintColor.None, primary);
		paintLookup.Add (PaintColor.RED, primary);
		paintLookup.Add (PaintColor.BLUE, primary);
		paintLookup.Add (PaintColor.YELLOW, primary);
		
		// secondary colors
		List<PaintColor> purple = new List<PaintColor> ();
		purple.Add (PaintColor.BLUE);
		purple.Add (PaintColor.RED);
		paintLookup.Add (PaintColor.PURPLE, purple);
		
		List<PaintColor> green = new List<PaintColor>();
		green.Add (PaintColor.BLUE);
		green.Add (PaintColor.YELLOW);
		paintLookup.Add (PaintColor.GREEN, green);
		
		List<PaintColor> orange = new List<PaintColor>();
		orange.Add (PaintColor.RED);
		orange.Add (PaintColor.YELLOW);
		paintLookup.Add (PaintColor.ORANGE, orange);
		
		// tertiary colors
		List<PaintColor> vermillion = new List<PaintColor>();
		vermillion.Add (PaintColor.RED);
		vermillion.Add (PaintColor.ORANGE);
		paintLookup.Add (PaintColor.VERMILLION, vermillion);
		
		List<PaintColor> violet = new List<PaintColor> ();
		violet.Add (PaintColor.BLUE);
		violet.Add (PaintColor.PURPLE);
		paintLookup.Add (PaintColor.VIOLET, violet);
		
		List<PaintColor> teal = new List<PaintColor>();
		teal.Add (PaintColor.BLUE);
		teal.Add (PaintColor.GREEN);
		paintLookup.Add (PaintColor.TEAL, teal);
		
		List<PaintColor> amber = new List<PaintColor>();
		amber.Add (PaintColor.YELLOW);
		amber.Add (PaintColor.ORANGE);
		paintLookup.Add (PaintColor.AMBER, amber);
		
		List<PaintColor> magenta = new List<PaintColor>();
		magenta.Add (PaintColor.RED);
		magenta.Add (PaintColor.PURPLE);
		paintLookup.Add (PaintColor.MAGENTA, magenta);
		
		List<PaintColor> chartreuse = new List<PaintColor>();
		chartreuse.Add (PaintColor.YELLOW);
		chartreuse.Add (PaintColor.GREEN);
		paintLookup.Add (PaintColor.CHARTREUSE, chartreuse);
	}

	public static PaintColor mix(PaintColor color1, PaintColor color2) {
		PaintColor mix = PaintColor.None;

		if (!isPopulated) {
			populateColorMap();
		}

		if (color1 != color2) {
			foreach (KeyValuePair<PaintColor, List<PaintColor>> color in paintLookup) {
				if (color.Value.Contains (color1) && color.Value.Contains (color2)) {
					return color.Key;
				}
			}	
		}

		return mix;
	}
}
