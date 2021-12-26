using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class AddToneRequest : RequestBase<Tone>
    {
        public string Name { get; set; }

        public double Frequency { get; set; }

        public double WaveLenght { get; set; }
    }
}
