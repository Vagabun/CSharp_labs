using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BitContainer {
    public class BitContainer : IEnumerable<bool> {
        private List<byte> data;
        private const int cellCapacity = 8;
        public int Length { get; private set; }
        private readonly Func<bool, int> convert = bit => bit ? 1 : 0;

        public BitContainer() {
            data = new List<byte>();
            Length = 0;
        }

        public int this[int index] {
            get => GetBit(index);
            set => SetBit(value, index);
        }

        public void PushBit(int bit) {
            if (bit != 0 && bit != 1)
                throw new ArgumentOutOfRangeException(nameof(bit),
                    "Error: this value can't be pushed into BitContainer");

            //push fictive byte in cell if offset equals 0
            if (Length % cellCapacity == 0)
                data.Add(0);
            SetBit(bit, Length);
            Length++;
        }

        public void PushBit(bool bit) {
            //int convertedBit = bit ? 1 : 0;
            PushBit(convert(bit));
        }

        private void SetBit(int bit, int position) {
            if (bit != 0 && bit != 1)
                throw new ArgumentOutOfRangeException(nameof(bit),
                    "Error: this value can't be inserted into BitContainer");
            if (position < 0 || position > Length)
                throw new IndexOutOfRangeException("Error: wrong index");

            int bytePlace = position / cellCapacity;
            int offset = position % cellCapacity;
            int currentByte = data[bytePlace];

            /* add bit to current byte */
            //~(1 << offset) - get bit mask, then apply it to current byte -> reset bit on selected position
            currentByte &= ~(1 << offset);
            //(currentByte | (bit << offset)) - write new bit on selected position
            data[bytePlace] = (byte)(currentByte | (bit << offset));
        }

        private void SetBit(bool bit, int position) {
            //int convertedBit = bit ? 1 : 0;
            SetBit(convert(bit), position);
        }

        private int GetBit(int position) {
            if (position < 0 || position > Length)
                throw new IndexOutOfRangeException("Error: wrong index");

            int place = position / cellCapacity;
            int offset = position % cellCapacity;
            //get bit from selected position by making bitwise AND with (1 << offset)
            int bit = (data[place] & (1 << offset)) > 0 ? 1 : 0;
            return bit;
        }

        public void Clear() {
            Length = 0;
            data.Clear();
        }

        public void Insert(int position, int bit) {
            if (position < 0 || position > Length)
                throw new IndexOutOfRangeException("Error: wrong index");
            if (bit != 0 && bit != 1)
                throw new ArgumentOutOfRangeException(nameof(bit),
                    "Error: this value can't be inserted into BitContainer");
            //push fictive bit
            PushBit(this[Length - 1]);
            for (int i = Length - 1; i > position; --i)
                this[i] = this[i - 1];
            this[position] = bit;
        }

        public void Insert(int position, bool bit) => Insert(position, convert(bit));

        public void Remove(int position) {
            if (position < 0 || position > Length)
                throw new IndexOutOfRangeException("Error: wrong index");
            for (int i = position; i < Length - 1; ++i)
                this[i] = this[i + 1];
            SetBit(0, Length - 1);
            Length--;
        }

        public override string ToString() {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < data.Count; ++i) {
                str.Append(Convert.ToString(data[i], toBase: 2));
                str.Append(" ");
            }
            str.Append("\n");
            return str.ToString();
        }

        public IEnumerator<bool> GetEnumerator() {
            for (int i = 0; i < Length; ++i)
                yield return Convert.ToBoolean(this[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
