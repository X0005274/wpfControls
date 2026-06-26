using System;
using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the Lot 조회 (lot search) screen's result grid.
    /// Columns: LotId / ProdId / ProduceType / ProdType / SubProdType /
    /// LastEventTime / LastEventCd / OperId. This is a UI shell model only; it
    /// carries no business logic.
    /// </summary>
    public class LotInfo : INotifyPropertyChanged
    {
        private string lotId;
        private string prodId;
        private string produceType;
        private string prodType;
        private string subProdType;
        private DateTime lastEventTime;
        private string lastEventCd;
        private string operId;

        public LotInfo()
        {
            this.lotId = string.Empty;
            this.prodId = string.Empty;
            this.produceType = string.Empty;
            this.prodType = string.Empty;
            this.subProdType = string.Empty;
            this.lastEventTime = DateTime.MinValue;
            this.lastEventCd = string.Empty;
            this.operId = string.Empty;
        }

        public string LotId
        {
            get { return this.lotId; }
            set
            {
                if (this.lotId != value)
                {
                    this.lotId = value;
                    this.OnPropertyChanged("LotId");
                }
            }
        }

        public string ProdId
        {
            get { return this.prodId; }
            set
            {
                if (this.prodId != value)
                {
                    this.prodId = value;
                    this.OnPropertyChanged("ProdId");
                }
            }
        }

        public string ProduceType
        {
            get { return this.produceType; }
            set
            {
                if (this.produceType != value)
                {
                    this.produceType = value;
                    this.OnPropertyChanged("ProduceType");
                }
            }
        }

        public string ProdType
        {
            get { return this.prodType; }
            set
            {
                if (this.prodType != value)
                {
                    this.prodType = value;
                    this.OnPropertyChanged("ProdType");
                }
            }
        }

        public string SubProdType
        {
            get { return this.subProdType; }
            set
            {
                if (this.subProdType != value)
                {
                    this.subProdType = value;
                    this.OnPropertyChanged("SubProdType");
                }
            }
        }

        public DateTime LastEventTime
        {
            get { return this.lastEventTime; }
            set
            {
                if (this.lastEventTime != value)
                {
                    this.lastEventTime = value;
                    this.OnPropertyChanged("LastEventTime");
                }
            }
        }

        public string LastEventCd
        {
            get { return this.lastEventCd; }
            set
            {
                if (this.lastEventCd != value)
                {
                    this.lastEventCd = value;
                    this.OnPropertyChanged("LastEventCd");
                }
            }
        }

        public string OperId
        {
            get { return this.operId; }
            set
            {
                if (this.operId != value)
                {
                    this.operId = value;
                    this.OnPropertyChanged("OperId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
