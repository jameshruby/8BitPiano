using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano.Model.Sequencer.ADSR
{
    struct Phase : IPhase
    {
        double duration;
        double start;
        double strength;

        public double Duration { get { return duration; } set { duration = value; } }
        public double Start { get { return start; } set { start = value; } }
        public double Strength { get { return strength; } set { strength = value; } }


        public Phase(double duration, double start, double end)
        {
            this.duration = duration;
            this.start = start;
            this.strength = end;
        }
    }


    public static class AttackPhase
    {
       public const double strength = 32767.0;
       public const double duration = (double)1 / 20;

    }

    public static class DecayPhase
    {
        public const double strength = 18000.0;
        public const double duration = (double)1 / 9;
    }

    public static class SustainPhase
    {
        public const double strength = 14000.0;
        public const double duration = (double)1 / 4;
    }

    public static class ReleasePhase
    {
        public const double strength = 0.0;
        public const double duration = (double)1 / 6;
    }
}
