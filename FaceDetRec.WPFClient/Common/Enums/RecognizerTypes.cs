using System.ComponentModel;

namespace FaceDetRec.WPFClient.Common.Enums
{
    public enum RecognizerTypes
    {
        [Description("Eigen Recognizer")]
        EigenRecognizer,

        [Description("Fisher Recognizer")]
        FisherFaceRecognizer,

        [Description("LBPH Recognizer")]
        LbphFaceRecognizer
    }
}
