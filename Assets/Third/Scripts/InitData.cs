[System.Serializable]
public class InitData
{
    public string appmetricaAppId_prop;
    public string oneSignalAppId_prop;
    public string appsFlyerAppId_prop;

    public InitData(string appmetricaAppId_prop, string oneSignalAppId_prop, string appsFlyerAppId_prop)
    {
        this.appmetricaAppId_prop = appmetricaAppId_prop;
        this.oneSignalAppId_prop = oneSignalAppId_prop;
        this.appsFlyerAppId_prop = appsFlyerAppId_prop;
    }
}
