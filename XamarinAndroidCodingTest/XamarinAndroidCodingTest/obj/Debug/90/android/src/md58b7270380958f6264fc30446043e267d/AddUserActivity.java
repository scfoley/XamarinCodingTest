package md58b7270380958f6264fc30446043e267d;


public class AddUserActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("XamarinAndroidCodingTest.UI.Activities.AddUserActivity, XamarinAndroidCodingTest", AddUserActivity.class, __md_methods);
	}


	public AddUserActivity ()
	{
		super ();
		if (getClass () == AddUserActivity.class)
			mono.android.TypeManager.Activate ("XamarinAndroidCodingTest.UI.Activities.AddUserActivity, XamarinAndroidCodingTest", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
