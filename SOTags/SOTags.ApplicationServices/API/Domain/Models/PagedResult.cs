namespace SOTags.ApplicationServices.API.Domain.Models
{
    public class PagedResult
    {
        public List<Tag> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PagedResult(List<Tag> items, int totalCount, int pageSize, int pageNumber, string? sortByName, string? sortByCount)
        {
            TotalItemsCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = (pageNumber - 1) * pageSize + 1;
            ItemsTo = Math.Min(pageNumber * pageSize, totalCount);
            Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            ApplySorting(sortByName, sortByCount);
        }

        private void ApplySorting(string? sortByName, string? sortByCount)
        {
            if (sortByName != null && sortByCount == null)
            {
                SortByName(sortByName);
            }

            else if (sortByName == null && sortByCount != null)
            {
                SortByCount(sortByCount);
            }

            else if (sortByName != null && sortByCount != null)
            {
                SortByNameThanByCount(sortByName, sortByCount);
            }
        }

        private void SortByNameThanByCount(string sortByName, string sortByCount)
        {
            if ("ASC".Equals(sortByName.ToUpper()) && "ASC".Equals(sortByCount.ToUpper()))
            {
                Items = Items
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Count)
                    .ToList();
            }
            else if ("DESC".Equals(sortByName.ToUpper()) && "ASC".Equals(sortByCount.ToUpper()))
            {
                Items = Items
                    .OrderByDescending(x => x.Name)
                    .ThenBy(x => x.Count)
                    .ToList();
            }
            else if ("ASC".Equals(sortByName.ToUpper()) && "DESC".Equals(sortByCount.ToUpper()))
            {
                Items = Items
                    .OrderBy(x => x.Name)
                    .ThenByDescending(x => x.Count)
                    .ToList();
            }
            else if ("DESC".Equals(sortByName.ToUpper()) && "DESC".Equals(sortByCount.ToUpper()))
            {
                Items = Items
                    .OrderByDescending(x => x.Name)
                    .ThenByDescending(x => x.Count)
                    .ToList();
            }
            else
            {
                throw new Exception("INVALID COMMAND");
            }
        }

        private void SortByCount(string sortByCount)
        {
            if ("ASC".Equals(sortByCount.ToUpper()))
            {
                Items = Items.OrderBy(x => x.Count).ToList();
            }
            else if ("DESC".Equals(sortByCount.ToUpper()))
            {
                Items = Items.OrderByDescending(x => x.Count).ToList();
            }
            else
            {
                throw new Exception("INVALID COMMAND");
            }
        }

        private void SortByName(string sortByName)
        {
            if ("ASC".Equals(sortByName.ToUpper()))
            {
                Items = Items.OrderBy(x => x.Name).ToList();
            }
            else if ("DESC".Equals(sortByName.ToUpper()))
            {
                Items = Items.OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                throw new Exception("INVALID COMMAND");
            }
        }
    }
}
