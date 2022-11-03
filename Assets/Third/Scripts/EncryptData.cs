using System.Collections.Generic;

[System.Serializable]
public class EncryptData
{
	public string protocol;

	public string sim_geo;

	public string bundle;

	public string amidentificator;

    public string afidentificator;

    public string googleid;

	public string subcodename;

	public string appref;

	public string dl_url;

	public string dl_url2;

	public string bundle_prop;

	public string subcodename_prop;

	public string domen_prop;

	public string space_prop;

	public string requestCampaign_prop;

    public static bool operator ==(EncryptData first, EncryptData second) => first == second;
    public static bool operator !=(EncryptData first, EncryptData _) => first.sim_geo != "[none]";

    public EncryptData()
    {
        protocol = "[none]";
        sim_geo = "[none]";
        bundle = "[none]";
        amidentificator = "[none]";
        afidentificator = "[none]";
        googleid = "[none]";
        subcodename = "[none]";
        appref = "[none]";
        dl_url = "[none]";
        dl_url2 = "[none]";
        bundle_prop = "[none]";
        subcodename_prop = "[none]";
        domen_prop = "[none]";
        space_prop = "[none]";
        requestCampaign_prop = "[none]";
    }

    public EncryptData(string protocol, string sim_geo, string bundle, string amidentificator, string afidentificator, string googleid, string subcodename, string appref, string dl_url, string dl_url2, string bundle_prop, string subcodename_prop, string domen_prop, string space_prop, string requestCampaign_prop):this()
    {
        this.protocol = protocol;
        this.sim_geo = sim_geo;
        this.bundle = bundle;
        this.amidentificator = amidentificator;
        this.afidentificator = afidentificator;
        this.googleid = googleid;
        this.subcodename = subcodename;
        this.appref = appref;
        this.dl_url = dl_url;
        this.dl_url2 = dl_url2;
        this.bundle_prop = bundle_prop;
        this.subcodename_prop = subcodename_prop;
        this.domen_prop = domen_prop;
        this.space_prop = space_prop;
        this.requestCampaign_prop = requestCampaign_prop;
    }

    public override bool Equals(object obj)
    {
        return obj is EncryptData data &&
               protocol == data.protocol &&
               sim_geo == data.sim_geo &&
               bundle == data.bundle &&
               amidentificator == data.amidentificator &&
               afidentificator == data.afidentificator &&
               googleid == data.googleid &&
               subcodename == data.subcodename &&
               appref == data.appref &&
               dl_url == data.dl_url &&
               dl_url2 == data.dl_url2 &&
               bundle_prop == data.bundle_prop &&
               subcodename_prop == data.subcodename_prop &&
               domen_prop == data.domen_prop &&
               space_prop == data.space_prop &&
               requestCampaign_prop == data.requestCampaign_prop;
    }

    public override int GetHashCode()
    {
        int hashCode = 135796883;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(protocol);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(sim_geo);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(bundle);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(amidentificator);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(afidentificator);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(googleid);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(subcodename);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(appref);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(dl_url);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(dl_url2);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(bundle_prop);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(subcodename_prop);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(domen_prop);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(space_prop);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(requestCampaign_prop);
        return hashCode;
    }
}
