using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    internal static class MinimumMeetingRooms
    {
        public static int MinClassrooms(int[][] intervals)
        {
            // Başlangıç zamanına göre sırala
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

            // Min-heap => bitiş zamanları
            PriorityQueue<int, int> pq = new();

            foreach (var interval in intervals)
            {
                int start = interval[0];
                int end = interval[1];

                // En erken biten ders bu dersten önce bittiyse
                // odayı tekrar kullan
                if (pq.Count > 0 && pq.Peek() <= start)
                {
                    pq.Dequeue();
                }

                // mevcut dersi ekle
                pq.Enqueue(end, end);
            }

            return pq.Count;
        }

    }
}
