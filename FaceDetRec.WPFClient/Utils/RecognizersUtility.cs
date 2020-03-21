using FaceDetRec.WPFClient.Common;
using FaceDetRec.WPFClient.DataModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using FaceDetRec.WPFClient.Common.Enums;

namespace FaceDetRec.WPFClient.Utils
{
    public static class RecognizersUtility
    {
        public static ObservableCollection<RecognizerModel> GetRecognizers()
        {
            var recognizers = new ObservableCollection<RecognizerModel>();
            var recognizerTypes = Enum.GetValues(typeof(RecognizerTypes));

            foreach (var recognizer in recognizerTypes)
            {
                recognizers.Add(new RecognizerModel
                {
                    Index = (int)recognizer,
                    Content = recognizer.GetDesciption()
                });
            }

            return recognizers;
        }

        public static RecognizerModel SetDefaultRecognizer(this ObservableCollection<RecognizerModel> availableRecognizers)
        {
            return availableRecognizers.FirstOrDefault();
        }
    }
}
