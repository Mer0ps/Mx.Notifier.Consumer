namespace Mx.Notifier.Consumer;
public static class TransactionEvents
{
    public static readonly string ESDT_NFT_Transfer = "ESDTNFTTransfer";
    public static readonly string ESDT_NFT_Burn = "ESDTNFTBurn";
    public static readonly string ESDT_NFT_ADD_QUANTITY = "ESDTNFTAddQuantity";
    public static readonly string ESDT_NFT_CREATE = "ESDTNFTCreate";
    public static readonly string MULTI_ESDT_NFT_TRANSFER = "MultiESDTNFTTransfer";
    public static readonly string ESDT_TRANSFER = "ESDTTransfer";
    public static readonly string ESDT_BURN = "ESDTBurn";
    public static readonly string ESDT_LOCAL_MINT = "ESDTLocalMint";
    public static readonly string ESDT_LOCAL_BURN = "ESDTLocalBurn";
    public static readonly string ESDT_WIPE = "ESDTWipe";
    public static readonly string ESDT_FREEZE = "ESDTFreeze";
    public static readonly string TRANSFER_VALUE_ONLY = "transferValueOnly";
    public static readonly string WRITE_LOG = "writeLog";
    public static readonly string SIGNAL_ERROR = "signalError";
    public static readonly string COMPLETE_TX = "completedTxEvent";
    public static readonly string INTERNAL_VM_ERRORS = "internalVMErrors";
}

