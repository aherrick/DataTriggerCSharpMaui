using CommunityToolkit.Mvvm.ComponentModel;

namespace DataTriggerCSharpMaui
{
    public enum LayoutState
    {
        Loading,
        NoConnection,
        Success,
        Error,
        Empty
    }

    public partial class VenuePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private LayoutState layoutState = LayoutState.Loading;

        public VenuePageViewModel()
        {
            LayoutState = LayoutState.Success;
        }
    }

    public class VenueSuccessContentView : ContentView
    {
        public VenueSuccessContentView()
        {
            Content = new Label() { Text = "hello world", TextColor = Colors.Red };
        }
    }

    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var venueSuccessCV = new VenueSuccessContentView();
            Resources.Add(nameof(LayoutState.Success), venueSuccessCV);

            var cvWrap = new ContentView();
            Content = cvWrap;

            var datatrigger = new DataTrigger(typeof(ContentView))
            {
                Binding = new Binding(path: nameof(VenuePageViewModel.LayoutState)),
                Value = LayoutState.Success,
                Setters = {
                new Setter { Property =  ContentView.ContentProperty, Value = venueSuccessCV },
            }
            };

            cvWrap.Triggers.Add(datatrigger);

            BindingContext = new VenuePageViewModel();
        }
    }
}