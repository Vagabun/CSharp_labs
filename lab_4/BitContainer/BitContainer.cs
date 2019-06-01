using System;
using System.Collections;
using System.Collections.Generic;

namespace BitContainer {
    public class BitContainer : IEnumerable<bool> {

        public IEnumerator<bool> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
