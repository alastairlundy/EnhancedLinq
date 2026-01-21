namespace EnhancedLinq.Immediate;

public static partial class EnhancedLinqImmediate
{
    extension<TSource>(List<TSource> list)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TSource> Exclude(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(predicate);
            
            List<TSource> output = new(list.Count);
            output.AddRange(list);

            foreach (TSource item in collection)
            {
                if (predicate(item))
                {
                    if(output.Contains(item))
                        output.Remove(item);
                }
            }
            
            return output;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public List<TSource> Exclude(ICollection<TSource> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            List<TSource> output = new(capacity: list.Count);
            output.AddRange(list);

            foreach (TSource item in collection)
            {
                if(output.Contains(item))
                    output.Remove(item);
            }
            
            return output;
        }
    }
}