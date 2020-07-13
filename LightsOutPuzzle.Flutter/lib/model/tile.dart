class Tile {
  int _row;
  int _column;

  int get row => _row;
  int get column => _column;
  bool imageVisible;
  bool previousState;
  bool get hasBeenFlipped => previousState != imageVisible;

  Tile(this._row, this._column, {visible = false})
      : this.imageVisible = visible,
        this.previousState = visible;

  void flip() {
    previousState = imageVisible;
    imageVisible = !imageVisible;
  }

  Tile clone() {
    return Tile(_row, _column, visible: imageVisible);
  }
}
