using Library;

public class BookCatalog
{
    private static BookCatalog instance;
    public static BookCatalog Instance
    {
        get
        {
            if (instance == null)
                instance = new();
            return instance;
        }
    }
    private CatalogNode Root { get; set; }

    private BookCatalog() { }

    public static void AddBook(Book book)
    {
        Instance.Root = CatalogNode.Insert(Instance.Root, book);
    }

    public static void RemoveBook(Book book)
    {

    }

    public static List<Book> GetFullCatalog()
    {
        List<Book> fullCatalog = new();
        CatalogNode.InOrder(fullCatalog, Instance.Root);
        return fullCatalog;
    }

    private class CatalogNode(Book value)
    {
        public Book Value { get; set; } = value;
        public CatalogNode Left { get; set; }
        public CatalogNode Right { get; set; }
        public int Height { get; set; } = 0;

        private static int GetHeight(CatalogNode node)
        {
            if (node == null)
                return -1;
            else
                return node.Height;
        }

        public static void UpdateHeight(CatalogNode node)
        {
            int left = GetHeight(node.Left);
            int right = GetHeight(node.Right);
            node.Height = int.Max(left, right) + 1;
        }

        public static int GetBalance(CatalogNode node)
        {
            if (node == null)
                return 0;
            if (node.Left == null && node.Right == null)
                return 0;
            if (node.Left == null)
                return -1 - node.Right.Height;
            if (node.Right == null)
                return node.Left.Height + 1;
            return node.Left.Height - node.Right.Height;
        }

        public static CatalogNode Insert(CatalogNode node, Book value)
        {
            if (node == null)
                return new CatalogNode(value);

            if (value.Id < node.Value.Id)
                node.Left = Insert(node.Left, value);
            else if (value.Id > node.Value.Id)
                node.Right = Insert(node.Right, value);
            else
                return node;

            return Rebalance(node);
        }

        public static CatalogNode Rebalance(CatalogNode node)
        {
            UpdateHeight(node);
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1)
            {
                if (GetBalance(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public static CatalogNode RotateLeft(CatalogNode node)
        {
            CatalogNode right = node.Right;
            CatalogNode rightLeft = right.Left;

            right.Left = node;
            node.Right = rightLeft;

            UpdateHeight(node);
            UpdateHeight(right);
            return right;
        }

        public static CatalogNode RotateRight(CatalogNode node)
        {
            CatalogNode left = node.Left;
            CatalogNode leftRight = left.Right;

            left.Right = node;
            node.Left = leftRight;

            UpdateHeight(node);
            UpdateHeight(left);
            return left;
        }

        public static void InOrder(List<Book> order, CatalogNode node)
        {
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
            {
                order.Add(node.Value);
                return;
            }

            InOrder(order, node.Left);
            order.Add(node.Value);
            InOrder(order, node.Right);
        }
    }
}