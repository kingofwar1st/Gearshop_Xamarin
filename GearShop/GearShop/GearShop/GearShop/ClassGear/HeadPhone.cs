using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GearShop.ClassGear
{
    public class HeadPhone : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private decimal? _warranty;
        public decimal? Warranty
        {
            get => _warranty;
            set
            {
                _warranty = value;
                OnPropertyChanged(nameof(Warranty));
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public decimal? _status;
        public decimal? Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private string _hinh;
        public string Hinh
        {
            get => _hinh;
            set
            {
                _hinh = value;
                OnPropertyChanged(nameof(Hinh));
            }
        }

        private Brand _brand;
        public Brand Brands
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brands));
            }
        }


        private Guid _brandId;
        public Guid BrandId
        {
            get => _brandId;
            set
            {
                _brandId = value;
                OnPropertyChanged(nameof(BrandId));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
