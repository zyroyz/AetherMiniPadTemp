using System;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class ExtGradient
    {
        public GradientColorKey[] colors = new GradientColorKey[]
        {
            new GradientColorKey(new Color(216f / 255f, 191f / 255f, 165f / 255f), 1f),
        };

        public bool isRainbow = false;
        public bool copyRigColors = false;
    }
}
