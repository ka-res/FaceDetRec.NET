using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface IFaceRecogntionService
    {
        bool TrainRecognizer(int recognizerIndex);
        int RecognizePerson(Image<Gray, byte> userImage, int recognizerIndex);
        void RetrainRecognizer();
        void SetRecognizer();
        void ShouldAlwaysRetrain();
    }
}
