# 3DPuzzle

_命名規則_
private, protected : キャメルケース public : パスカルケース const, readonly : コンスタントケース

_関数前と定数前にはsummaryを付け参照先でも見やすく表示_

__controller肥大化を防ぐため、関数はMoveに、変数はBaseクラスに記入__
Move関数が肥大化する場合はMove関数を分けて記入可

__Buildする際はエラーをはく可能性がある為別途ファイルを作成して行う__
