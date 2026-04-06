using IMS.Application.Inventories.Interfaces;
using IMS.Domain;

namespace IMS.Application.Inventories;

public class EditInventoryUseCase(IInventoryRepository inventoryRepository) : IEditInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    public async Task ExecuteAsync(Inventory inventory)
    {
        await inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
