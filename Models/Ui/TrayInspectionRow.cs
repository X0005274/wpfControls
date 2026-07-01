using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the tray inspection / judge grid (트레이 검사·판정 목록).
    /// Columns: DurableId / TrayType / SlotNo / FingerId / Timekey / LotId / WfId /
    /// ChipLotId / ChipWfId / LamellaLotId / LamellaWfId / JudgeResult.
    /// All values are strings so the model can be filled either from hard-coded
    /// sample data or straight from a JSON payload (JavaScriptSerializer maps JSON
    /// members onto these public properties by name). SlotNo / FingerId / JudgeResult
    /// are edited through combo-box columns, so this type raises PropertyChanged to
    /// keep the two-way bindings in sync. This is a UI shell model only; it carries
    /// no business logic.
    /// </summary>
    public class TrayInspectionRow : INotifyPropertyChanged
    {
        private string durableId;
        private string trayType;
        private string slotNo;
        private string fingerId;
        private string fingerIdx;
        private string timekey;
        private string lotId;
        private string wfId;
        private string chipLotId;
        private string chipWfId;
        private string lamellaLotId;
        private string lamellaWfId;
        private string judgeResult;

        public TrayInspectionRow()
        {
            this.durableId = string.Empty;
            this.trayType = string.Empty;
            this.slotNo = string.Empty;
            this.fingerId = string.Empty;
            this.fingerIdx = string.Empty;
            this.timekey = string.Empty;
            this.lotId = string.Empty;
            this.wfId = string.Empty;
            this.chipLotId = string.Empty;
            this.chipWfId = string.Empty;
            this.lamellaLotId = string.Empty;
            this.lamellaWfId = string.Empty;
            this.judgeResult = string.Empty;
        }

        public string DurableId
        {
            get { return this.durableId; }
            set
            {
                if (this.durableId != value)
                {
                    this.durableId = value;
                    this.OnPropertyChanged("DurableId");
                }
            }
        }

        public string TrayType
        {
            get { return this.trayType; }
            set
            {
                if (this.trayType != value)
                {
                    this.trayType = value;
                    this.OnPropertyChanged("TrayType");
                }
            }
        }

        public string SlotNo
        {
            get { return this.slotNo; }
            set
            {
                if (this.slotNo != value)
                {
                    this.slotNo = value;
                    this.OnPropertyChanged("SlotNo");
                }
            }
        }

        public string FingerId
        {
            get { return this.fingerId; }
            set
            {
                if (this.fingerId != value)
                {
                    this.fingerId = value;
                    this.OnPropertyChanged("FingerId");
                }
            }
        }

        public string FingerIdx
        {
            get { return this.fingerIdx; }
            set
            {
                if (this.fingerIdx != value)
                {
                    this.fingerIdx = value;
                    this.OnPropertyChanged("FingerIdx");
                }
            }
        }

        public string Timekey
        {
            get { return this.timekey; }
            set
            {
                if (this.timekey != value)
                {
                    this.timekey = value;
                    this.OnPropertyChanged("Timekey");
                }
            }
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

        public string WfId
        {
            get { return this.wfId; }
            set
            {
                if (this.wfId != value)
                {
                    this.wfId = value;
                    this.OnPropertyChanged("WfId");
                }
            }
        }

        public string ChipLotId
        {
            get { return this.chipLotId; }
            set
            {
                if (this.chipLotId != value)
                {
                    this.chipLotId = value;
                    this.OnPropertyChanged("ChipLotId");
                }
            }
        }

        public string ChipWfId
        {
            get { return this.chipWfId; }
            set
            {
                if (this.chipWfId != value)
                {
                    this.chipWfId = value;
                    this.OnPropertyChanged("ChipWfId");
                }
            }
        }

        public string LamellaLotId
        {
            get { return this.lamellaLotId; }
            set
            {
                if (this.lamellaLotId != value)
                {
                    this.lamellaLotId = value;
                    this.OnPropertyChanged("LamellaLotId");
                }
            }
        }

        public string LamellaWfId
        {
            get { return this.lamellaWfId; }
            set
            {
                if (this.lamellaWfId != value)
                {
                    this.lamellaWfId = value;
                    this.OnPropertyChanged("LamellaWfId");
                }
            }
        }

        public string JudgeResult
        {
            get { return this.judgeResult; }
            set
            {
                if (this.judgeResult != value)
                {
                    this.judgeResult = value;
                    this.OnPropertyChanged("JudgeResult");
                    this.OnPropertyChanged("JudgeDisplay");
                    this.OnPropertyChanged("JudgeTone");
                }
            }
        }

        /// <summary>Badge label: the result, or "미판정" while unjudged. (Not persisted.)</summary>
        public string JudgeDisplay
        {
            get
            {
                if (this.judgeResult == "SUCC" || this.judgeResult == "FAIL")
                {
                    return this.judgeResult;
                }

                return "미판정";
            }
        }

        /// <summary>Badge tone for <see cref="JudgeResult"/> (success/danger/neutral). (Not persisted.)</summary>
        public string JudgeTone
        {
            get
            {
                if (this.judgeResult == "SUCC")
                {
                    return "success";
                }

                if (this.judgeResult == "FAIL")
                {
                    return "danger";
                }

                return "neutral";
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
