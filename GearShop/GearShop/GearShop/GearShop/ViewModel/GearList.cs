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
    public class GearList : INotifyPropertyChanged
    { 
        public ObservableCollection<CPU> cPUs { get; set; }
        public ObservableCollection<CPU> cPUsChoose { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<KeyBoard> keyBoards { get; set; }
        public ObservableCollection<KeyBoard> keyBoardsChoose { get; set; }
        public ObservableCollection<HeadPhone> headPhones { get; set; }
        public ObservableCollection<HeadPhone> headPhonesChoose { get; set; }
        public string keyword = null;
        public Guid? guid = null;
        public int page = 1;


        public async Task GetHeadPhone()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var reponse = await httpClient.GetStringAsync($"api/headphone/getheadphone?guid={guid}&page={page}&keyword={keyword}");
            List<HeadPhone> headphones = JsonConvert.DeserializeObject<List<HeadPhone>>(reponse);
            for (int i = 0; i < headphones.Count; i++)
            {
                headphones[i].Hinh = ApiConfig.API_URL + headphones[i].Hinh;
                headPhones.Add(headphones[i]);
            }

        }


        public async Task GetKeyBoard()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var reponse = await httpClient.GetStringAsync($"api/keyboard/getkeyboard?guid={guid}&page={page}&keyword={keyword}");
            List<KeyBoard> keyboards = JsonConvert.DeserializeObject<List<KeyBoard>>(reponse);
            for (int i = 0; i < keyboards.Count; i++)
            {
                keyboards[i].Hinh = ApiConfig.API_URL + keyboards[i].Hinh;
                keyBoards.Add(keyboards[i]);
            }

        }


        public async Task GetCPU()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var response = await httpClient.GetStringAsync($"api/cpu/getcpu?guid={guid}&page={page}&keyword={keyword}");
            List<CPU> cpus = JsonConvert.DeserializeObject<List<CPU>>(response);
            
            for (int i = 0; i < cpus.Count; i++)
            {
                cpus[i].Hinh = ApiConfig.API_URL + cpus[i].Hinh;
                cPUs.Add(cpus[i]);
            }
        }
        public async Task GetBrand()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiConfig.API_URL);
            var reponse = await httpClient.GetStringAsync($"api/cpu/brand");
            var brands = JsonConvert.DeserializeObject<List<Brand>>(reponse);
            for(int i=0;i<brands.Count;i++)
            {
                Brands.Add(brands[i]);
            }
        }
        public GearList()
        {
            GetCPU();
            GetHeadPhone();
            GetKeyBoard();
            keyBoards = new ObservableCollection<KeyBoard>();
            headPhones = new ObservableCollection<HeadPhone>();
            cPUs = new ObservableCollection<CPU>();
            Brands = new ObservableCollection<Brand>();
            cPUsChoose = new ObservableCollection<CPU>();
            keyBoardsChoose = new ObservableCollection<KeyBoard>();
            headPhonesChoose = new ObservableCollection<HeadPhone>();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}