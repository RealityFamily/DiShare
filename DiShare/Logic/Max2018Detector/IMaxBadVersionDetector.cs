using DiShare.Infrastructure;


namespace DiShare.Logic.Max2018Detector
{
    public interface IMaxBadVersionDetector
    {
        TryResult<bool> Detect();
    }
}