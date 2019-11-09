using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    public class DefaultInstrumentNote //TODO fix accessibility + rename to InstrumentEnvelope?!
    {
        private double actualSound = 0.0;
        private Phase phase;

        private int SampleSize { get; set; }

        public double NoteDuration { get; set; }
        public Phase AttackPhase { get; }
        public Phase DecayPhase { get; }
        public Phase SustainPhase { get; }
        public Phase ReleasePhase { get; }
        public Phase EndPhase { get; }

        public List<double> PreparedTone { get => preparedTone; set => preparedTone = value; }

        //private List<double> preparedInstrumentADSRTone = new List<double>();
        private List<double> preparedTone;

        public DefaultInstrumentNote(int toneDuration)
        {
            preparedTone = new List<double>();
            //   var instrumentNote = new DefaultInstrumentNote(samplesSize);
            StreamAudioBuilder streamAudioBuilder = StreamAudioBuilder.GetInstance();
            this.SampleSize = streamAudioBuilder.GetSampleSize(toneDuration);

            AttackPhase = this.GetPhaseInstance<AttackPhase>();
            DecayPhase = this.GetPhaseInstance<DecayPhase>();
            SustainPhase = this.GetPhaseInstance<SustainPhase>();
            ReleasePhase = this.GetPhaseInstance<ReleasePhase>();
            EndPhase = this.GetPhaseInstance<EndPhase>();

            this.phase = AttackPhase;

            WriteADSRPhaseToStream(this.SampleSize);
        }

        private void WriteADSRPhaseToStream(int samplesSize)
        {
            //TODO FIX StreamAudioBuilder => InstrumentNote rel + depenednecies
            for (int T = 0; T < samplesSize; T++)
            {
                ToNextNote(T);
                this.PreparedTone.Add(CurrentNote);
            }
        }

        public Phase GetPhaseInstance<T>() where T : Phase, new()
        {
            //Create instance and increment note duration
            Phase phase = new T();
            phase.Instrument = this;

            //TODO prop visibility, who calls/owns what
            phase.DurationSampled = PhaseDuration(phase.Duration);
            phase.UpperLimit = phase.DurationSampled;

            NoteDuration += phase.Duration;
            return phase;
        }

        public Phase Phase { get { return phase; } set { phase = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        internal double PhaseDuration(double duration)
        {
            return this.SampleSize * duration;
        }

        internal void ToNextNote(int limit) //not sure bout naming
        {
            phase.NextNote(limit);
        }
    }
}
