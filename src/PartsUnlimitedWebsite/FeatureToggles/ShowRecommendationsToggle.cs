namespace PartsUnlimited.FeatureToggles
{
    using FeatureSwitch;
    using FeatureSwitch.Strategies;

    [AppSettings(Key = "ShowRecommendations")]
    public sealed class ShowRecommendationsToggle : BaseFeature
    {
    }
}
