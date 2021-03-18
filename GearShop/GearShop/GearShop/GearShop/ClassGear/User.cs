using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GearShop.ClassGear
{
    public class UserInformation : INotifyPropertyChanged
    {
        private string _hoTen;
        public string HoTen
        {
            get => _hoTen;
            set
            {
                _hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }
        private string _sdt;
        public string Sdt
        {
            get => _sdt;
            set
            {
                _sdt = value;
                OnPropertyChanged(nameof(Sdt));
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _pass;
        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                OnPropertyChanged(nameof(Pass));
            }
        }
        private Guid _userId;
        public Guid UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
