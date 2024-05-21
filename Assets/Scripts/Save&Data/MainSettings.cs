
namespace SaveData
{
    [System.Serializable]
    public class MainSettings
    {
        public bool _isFullScreen;
        public int _currentQualityLevel;
        public float _volumeValue;

        public int _width;
        public int _height;

        public bool[] _isOwned;

        public MainSettings()
        {
            _isFullScreen = true;
            _volumeValue = 0.5f;
            _currentQualityLevel = 2;
            _width = 1920;
            _height = 1080;
        }
    }
}


