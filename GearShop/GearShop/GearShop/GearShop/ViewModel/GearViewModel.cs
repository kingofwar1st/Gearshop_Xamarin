using GearShop.ClassGear;
using GearShop.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GearShop.ViewModel
{
    public class GearViewModel : INotifyPropertyChanged
    {

        private CPU _cpu;

        public CPU CPU
        {
            get => _cpu;
            set
            {
                _cpu = value;
                OnPropertyChanged(nameof(CPU));
            }
        }
        private KeyBoard _keyboard;

        public KeyBoard keyBoard
        {
            get => _keyboard;
            set
            {
                _keyboard = value;
                OnPropertyChanged(nameof(keyBoard));
            }
        }

        private HeadPhone _headphone;
        public HeadPhone headPhone
        {
            get => _headphone;
            set
            {
                _headphone = value;
                OnPropertyChanged(nameof(headPhone));
            }
        }

        public ObservableCollection<Brand> Brandss { get; set; }
        public ObservableCollection<CPU> CPUs { get; set; }
        public ObservableCollection<KeyBoard> KeyBoards { get; set; }
        public ObservableCollection<HeadPhone> HeadPhones { get; set; }

        public async Task GetBrand()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var reponse = await httpClient.GetStringAsync($"api/cpu/brand");
            var brands = JsonConvert.DeserializeObject<List<Brand>>(reponse);
            for (int i = 0; i < brands.Count; i++)
            {
                Brandss.Add(brands[i]);
            }
        }
        public GearViewModel()
        {
            Brandss = new ObservableCollection<Brand>();
            CPUs = new ObservableCollection<CPU>();
            KeyBoards = new ObservableCollection<KeyBoard>();
            HeadPhones = new ObservableCollection<HeadPhone>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
