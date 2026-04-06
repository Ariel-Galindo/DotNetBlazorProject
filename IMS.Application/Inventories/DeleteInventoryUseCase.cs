using IMS.Application.Inventories.Interfaces;

namespace IMS.Application.Inventories;

public class DeleteInventoryUseCase(IInventoryRepository inventoryRepository) : IDeleteInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task ExecuteAsync(int inventoryID)
    {
        await inventoryRepository.DeleteInventoryByIdAsync(inventoryID);
    }
}
