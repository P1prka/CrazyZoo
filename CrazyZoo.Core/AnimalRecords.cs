namespace CrazyZoo.Core;

public class GroupRecord
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public List<AnimalRecord> Animals { get; set; } = [];
}

public abstract class AnimalRecord
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public int Energy { get; set; }

    public int GroupId { get; set; }

    public GroupRecord? Group { get; set; }
}

public class LionRecord : AnimalRecord { }

public class ZebraRecord : AnimalRecord { }

public class GiraffeRecord : AnimalRecord { }

public class HippoRecord : AnimalRecord { }

public class PenguinRecord : AnimalRecord { }

public class MonkeyRecord : AnimalRecord { }

public class FoxRecord : AnimalRecord { }

public class PandaRecord : AnimalRecord { }

public class ElephantRecord : AnimalRecord { }