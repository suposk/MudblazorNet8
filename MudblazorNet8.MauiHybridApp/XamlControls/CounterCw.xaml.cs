namespace MudblazorNet8.MauiHybridApp.XamlControls;

public partial class CounterCw : ContentView
{
	public CounterCw()
	{
		InitializeComponent();
	}
    public int Counter { get; set; } = 0;

    private void btnTest_Clicked(object sender, EventArgs e)
    {
        Counter++;
        txt.Text = $"You clicked {Counter} times";
    }
}