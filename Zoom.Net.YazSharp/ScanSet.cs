/*
 * Copyright (c) 2005, Talis Information Limited.
 *
 * Permission to use, copy, modify, distribute, and sell this software and
 * its documentation, in whole or in part, for any purpose, is hereby granted,
 * provided that:
 *
 * 1. This copyright and permission notice appear in all copies of the
 * software and its documentation. Notices of copyright or attribution
 * which appear at the beginning of any file must remain unchanged.
 *
 * 2. The names of BLCMP, Talis Information Limited or the individual authors
 * may not be used to endorse or promote products derived from this software
 * without specific prior written permission.
 *
 * 3. Users of this software agree to make their best efforts, when
 * documenting their use of the software, to acknowledge Zoom.Net
 * and the role played by the software in their work.
 *
 * THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS, IMPLIED, OR OTHERWISE, INCLUDING WITHOUT LIMITATION, ANY
 * WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
 * IN NO EVENT SHALL INDEX DATA BE LIABLE FOR ANY SPECIAL, INCIDENTAL,
 * INDIRECT OR CONSEQUENTIAL DAMAGES OF ANY KIND, OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER OR
 * NOT ADVISED OF THE POSSIBILITY OF DAMAGE, AND ON ANY THEORY OF
 * LIABILITY, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE
 * OF THIS SOFTWARE.
 *
 */
using System;

namespace Zoom.Net.YazSharp
{
	public class ScanSet : IScanSet
	{
		internal ScanSet(IntPtr scanSet, Connection connection)
		{
			_connection = connection;
			_scanSet = scanSet;
		}

		~ScanSet()
		{
			Dispose();
		}

		public IScanTerm this[uint index]
		{
			get
			{
				int occ, length;
				string term = Yaz.ZOOM_scanset_term(_scanSet, index, out occ, out length);
				return new ScanTerm(term, occ);
			}
		}

		public uint Size
		{
			get
			{
				return Yaz.ZOOM_scanset_size(_scanSet);
			}
		}

		private Connection _connection;
		private IntPtr _scanSet;

		private bool _disposed = false;
		public void Dispose()
		{
			if (!_disposed)
			{
				Yaz.ZOOM_scanset_destroy(_scanSet);
				_connection = null;
				_scanSet = IntPtr.Zero;
				_disposed = true;
			}
		}
	}
}
