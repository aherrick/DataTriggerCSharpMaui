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

    public class MainPageCS : ContentPage
    {
        public MainPageCS()
        {
            var venueSuccessCV = new VenueSuccessContentView();
            Resources.Add(nameof(LayoutState.Success), venueSuccessCV);

            var cvWrap = new ContentView();
            var cv = new ContentView();
            cvWrap.Content = cv;

            Content = cvWrap;

            var datatrigger = new DataTrigger(typeof(ContentView))
            {
                Binding = new Binding(source: RelativeBindingSource.TemplatedParent, path: nameof(VenuePageViewModel.LayoutState)),
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