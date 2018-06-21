using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace RoomBookTest
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            GetJson();
        }
        public ObservableCollection<RoomObject> Items { get; set; } = new ObservableCollection<RoomObject>();
        private async void GetJson()
        {
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("json.txt");
            string json = await Windows.Storage.FileIO.ReadTextAsync(file);
            var item = JsonConvert.DeserializeObject<Item>(json);
            var roomitem = new RoomObject(item);
            Items.Add(roomitem);
        }


    }
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Boolean.Parse(value.ToString()))
            {
                return new SolidColorBrush(Colors.Green);
            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class RoomObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }

        }
        public RoomObject(Item dataSourece)
        {
            this.dataSourece = dataSourece;
            ModifyAvilable();
        }

        private void ModifyAvilable()
        {
            this.Name = dataSourece.room.name;
            DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(10) };
            timer.Tick += (s, e) =>
            {
                this.IsAvailable = true;
                timer.Stop();
            };
            timer.Start();

        }


        public Item dataSourece;

        private bool _isAvailable;
        public bool IsAvailable
        {
            get
            {
                return _isAvailable;
            }
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

    }
}