using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Repository.Entities;
using Repository.Interfaces;

public class ShoppingListService : IService<ShoppingListDto>
{
    private readonly IRepository<ShoppingList> _shoppingListRepository;
    private readonly IRepository<ShoppingListItem> _shoppingListItemRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    private int _userId;

    public void SetUserId(int userId)
    {
        _userId = userId;
    }

    public ShoppingListService(
        IRepository<ShoppingList> shoppingListRepository,
        IRepository<ShoppingListItem> shoppingListItemRepository,
        IRepository<Product> productRepository,
        IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _shoppingListItemRepository = shoppingListItemRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public ShoppingListDto AddItem(ShoppingListDto itemDto)
    {
        var entity = _mapper.Map<ShoppingList>(itemDto);
        entity.UserId = _userId;

        // שלב 1: שמירת רשימת קניות ללא הפריטים
        entity.ShoppingListItems = null;
        var addedList = _shoppingListRepository.Add(entity);

        // שלב 2: הכנסת הפריטים עם זיהוי ProductId לפי ברקוד
        foreach (var item in itemDto.Items)
        {
            var product = _productRepository
                .GetAll()
                .FirstOrDefault(p => p.Barcode == item.Barcode);

            if (product == null)
                throw new Exception($"Product with barcode {item.Barcode} not found.");

            var itemEntity = _mapper.Map<ShoppingListItem>(item);
            itemEntity.ProductId = product.Id; // עדכון ה־FK הנכון
            itemEntity.ListId = addedList.Id;

            _shoppingListItemRepository.Add(itemEntity);
        }

        return _mapper.Map<ShoppingListDto>(addedList);
    }

    public void Delete(int id)
    {
        var list = _shoppingListRepository.GetById(id);
        if (list == null || list.UserId != _userId)
            throw new Exception("Shopping list not found");

        var relatedItems = _shoppingListItemRepository.GetAll()
            .Where(item => item.ListId == id)
            .ToList();

        foreach (var item in relatedItems)
        {
            _shoppingListItemRepository.Delete(item.Id);
        }

        _shoppingListRepository.Delete(id);
    }

    public List<ShoppingListDto> GetAll()
    {
        var allLists = _shoppingListRepository.GetAll()
            .Where(l => l.UserId == _userId)
            .ToList();

        return _mapper.Map<List<ShoppingListDto>>(allLists);
    }

    public ShoppingListDto GetById(int id)
    {
        var list = _shoppingListRepository.GetById(id);
        if (list == null || list.UserId != _userId)
            throw new Exception("Shopping list not found");

        return _mapper.Map<ShoppingListDto>(list);
    }

    public ShoppingListDto Update(int id, ShoppingListDto item)
    {
        var existing = _shoppingListRepository.GetById(id);
        if (existing == null || existing.UserId != _userId)
            throw new Exception("Shopping list not found");

        existing.Title = item.Title;

        var updated = _shoppingListRepository.Put(id, existing);
        return _mapper.Map<ShoppingListDto>(updated);
    }
}
