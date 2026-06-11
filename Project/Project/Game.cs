using System.ComponentModel;

public class Game : IEditableObject
{
    int id;
    string title;
    int idGenre;
    int idPlatform;
    int idStatus;
    int rating;
    int hoursPlayed;
    string notes;
    DateTime dateAdded;
    bool toDelete;

    // backup fields
    int _id;
    string _title;
    int _idGenre;
    int _idPlatform;
    int _idStatus;
    int _rating;
    int _hoursPlayed;
    string _notes;
    DateTime _dateAdded;
    bool _editing;
    bool _toDelete;

    public int Id { get => id; set => id = value; }
    public string Title { get => title; set => title = value; }
    public int IdGenre { get => idGenre; set => idGenre = value; }
    public int IdPlatform { get => idPlatform; set => idPlatform = value; }
    public int IdStatus { get => idStatus; set => idStatus = value; }
    public int Rating { get => rating; set => rating = value; }
    public int HoursPlayed { get => hoursPlayed; set => hoursPlayed = value; }
    public string Notes { get => notes; set => notes = value; }
    public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
    public bool ToDelete { get => toDelete; set => toDelete = value; }
    public Game(int id, string title, int idGenre, int idPlatform, int idStatus, int rating, int hoursPlayed, string notes, DateTime dateAdded, bool toDelete)
    {
        Id = id;
        Title = title;
        IdGenre = idGenre;
        IdPlatform = idPlatform;
        IdStatus = idStatus;
        Rating = rating;
        HoursPlayed = hoursPlayed;
        Notes = notes;
        DateAdded = dateAdded;
        ToDelete = toDelete;
    }

    public override string ToString() => title;

    public void BeginEdit()
    {
        if (_editing) return;
        _editing = true;
        _id = id;
        _title = title;
        _idGenre = idGenre;
        _idPlatform = idPlatform;
        _idStatus = idStatus;
        _rating = rating;
        _hoursPlayed = hoursPlayed;
        _notes = notes;
        _dateAdded = dateAdded;
        _toDelete = toDelete;
    }

    public void CancelEdit()
    {
        if (!_editing) return;
        _editing = false;
        id = _id;
        title = _title;
        idGenre = _idGenre;
        idPlatform = _idPlatform;
        idStatus = _idStatus;
        rating = _rating;
        hoursPlayed = _hoursPlayed;
        notes = _notes;
        dateAdded = _dateAdded;
        toDelete = _toDelete;
    }

    public void EndEdit()
    {
        _editing = false;
    }
}