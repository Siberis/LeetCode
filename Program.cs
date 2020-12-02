using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace LeetCode
{
    public class Solution1
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[0];
        }
        [Fact]
        public void TwoSumTest()
        {
            Assert.Equal(new int[] { 0, 1 }, Solution1.TwoSum(new int[] { 2, 7, 11, 15 }, 9));
            Assert.Equal(new int[] { 1, 2 }, Solution1.TwoSum(new int[] { 3, 2, 4 }, 6));
            Assert.Equal(new int[] { 0, 1 }, Solution1.TwoSum(new int[] { 3, 3 }, 6));
        }
    }
    public class Solution2
    {
        public class ListNode : EqualityComparer<ListNode>, IEquatable<ListNode>
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }

            public bool Equals(ListNode other)
            {
                return val == other.val && (next != null ? Equals(next, other.next) : true);
            }

            public override bool Equals(ListNode x, ListNode y)
            {
                return x.val == y.val && (x.next != null ? Equals(x.next, y.next) : true);
            }

            public override int GetHashCode([DisallowNull] ListNode obj)
            {
                throw new NotImplementedException();
            }
        }
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var a = new List<int>();
            var b = new List<int>();
            while (l1 != null)
            {
                a.Add(l1.val);
                l1 = l1.next;
            }
            while (l2 != null)
            {
                b.Add(l2.val);
                l2 = l2.next;
            }

            var arr = new List<int>();
            var carry = 0;
            for (int i = 0; i < Math.Max(a.Count, b.Count) + 1; i++)
            {
                var sum = (a.Count > i ? a[i] : 0) + (b.Count > i ? b[i] : 0) + carry;
                if (sum >= 10)
                {
                    carry = sum / 10;
                    arr.Add(sum % 10);
                }
                else
                {
                    carry = 0;
                    arr.Add(sum);
                }
            }
            if (carry != 0)
            {
                arr.Add(carry);
            }
            arr.Reverse();
            arr = arr.SkipWhile(e => e == 0).ToList();
            if (arr.Count == 0)
            {
                arr.Add(0);
            }
            ListNode current = null;
            foreach (var i in arr)
            {
                var x = new ListNode(i, null);
                if (current == null)
                {
                    current = x;
                }
                else
                {
                    x.next = current;
                    current = x;
                }
            }
            return current;
        }

        [Fact]
        public void AddTwoNumbersTest()
        {
            Assert.Equal(FromArray(new int[] { 7, 0, 8 }), Solution2.AddTwoNumbers(FromArray(new int[] { 2, 4, 3 }), FromArray(new int[] { 5, 6, 4 })));
            Assert.Equal(FromArray(new int[] { 0 }), Solution2.AddTwoNumbers(FromArray(new int[] { 0 }), FromArray(new int[] { 0 })));
            Assert.Equal(FromArray(new int[] { 8, 9, 9, 9, 0, 0, 0, 1 }), Solution2.AddTwoNumbers(FromArray(new int[] { 9, 9, 9, 9, 9, 9, 9 }), FromArray(new int[] { 9, 9, 9, 9 })));
            Assert.Equal(FromArray(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }), Solution2.AddTwoNumbers(FromArray(new int[] { 1, 9, 9, 9, 9, 9, 9, 9, 9, 9 }), FromArray(new int[] { 9 })));
        }
        private ListNode FromArray(int[] array)
        {
            ListNode current = null;
            foreach (var i in array.Reverse())
            {
                var x = new ListNode(i, null);
                if (current == null)
                {
                    current = x;
                }
                else
                {
                    x.next = current;
                    current = x;
                }
            }
            return current;
        }
    }
    public class Solution3
    {
        public static int LengthOfLongestSubstring(string s)
        {
            if (s.Length == 0) return 0;
            if (s.Length == 1) return 1;
            var max = 1;
            for (int i = 0; i < s.Length; ++i)
            {
                var next = s.IndexOf(s[i], i + 1);
                for (int j = next == -1 ? s.Length - 1 : next; j >= i + max; j--)
                {
                    if (s.Substring(i, j - i + 1).Distinct().Count() == j - i + 1)
                    {
                        max = j - i + 1;
                    }
                }
            }
            return max;
        }

        [Fact]
        public void LengthOfLongestSubstringTest()
        {
            Assert.Equal(3, Solution3.LengthOfLongestSubstring("dvdf"));
            Assert.Equal(3, Solution3.LengthOfLongestSubstring("abcabcbb"));
            Assert.Equal(1, Solution3.LengthOfLongestSubstring("bbbbb"));
            Assert.Equal(3, Solution3.LengthOfLongestSubstring("pwwkew"));
            Assert.Equal(0, Solution3.LengthOfLongestSubstring(""));
            Assert.Equal(1, Solution3.LengthOfLongestSubstring(" "));
            Assert.Equal(2, Solution3.LengthOfLongestSubstring("au"));
            Assert.Equal(94, Solution3.LengthOfLongestSubstring(@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ "));
        }
    }
    public class Solution4
    {
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var sum = new int[nums1.Length + nums2.Length];
            var a = 0;
            var b = 0;
            for (int i = 0; i < sum.Length; i++)
            {
                if (a >= nums1.Length)
                {
                    sum[i] = nums2[b];
                    b++;
                    continue;
                }
                if (b >= nums2.Length)
                {
                    sum[i] = nums1[a];
                    a++;
                    continue;
                }
                if (nums1[a] > nums2[b])
                {
                    sum[i] = nums2[b];
                    b++;
                }
                else
                {
                    sum[i] = nums1[a];
                    a++;
                }
            }
            if (sum.Length % 2 == 1)
            {
                return sum[sum.Length / 2];
            }
            else
            {
                return (double)((sum[(sum.Length / 2) - 1] + sum[sum.Length / 2])) / 2;
            }
        }

        [Fact]
        public void FindMedianSortedArraysTest()
        {
            Assert.Equal(2, Solution4.FindMedianSortedArrays(new int[] { 1, 3 }, new int[] { 2 }));
            Assert.Equal(2.5, Solution4.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 3, 4 }));
            Assert.Equal(0, Solution4.FindMedianSortedArrays(new int[] { 0, 0 }, new int[] { 0, 0 }));
            Assert.Equal(1, Solution4.FindMedianSortedArrays(new int[] { }, new int[] { 1 }));
            Assert.Equal(2, Solution4.FindMedianSortedArrays(new int[] { 2 }, new int[] { }));
        }
    }
}
