## 13 Red  Black Tree

> **properties**:
>
> - root is black
> - leaf(NIL) is black
> - no continuous red nodes
> - all simple paths from the node to descendant leaves contain the same number of black nodes.

### **Lemma 13.1 **

> A red-black tree with n internal nodes has height at most `2lg(n+1)`



***



## Rotations

> ```
> LEFT-ROTATE(T,x)
> t=x.right
> x.right=y.left
> if y.left!=T.nil
>    y.left.p=x
>    y.p=x.p
>    if x.p==T.nil
>       T.root=y
>    else if x==x.p.left
>         x.p.left=y
>         else
>         x.p.right=y
> y.left=x
> x.p=y
> ```



***

## Insertion
> same as Insert in chapter 12 except that :
> > - NIL -> T.nil
> > - z set color
> > - z.left &&z.right are set to T.nil
> > - RB-INSERT-FIXUP

> ```c++
> RB-INSET-FIXUP(T,z)
> while z.p.color==RED
>       if z.p==z.p.p.left
>          y=z.p.p.right
>          if y.color==RED
>             z.p.color =BLACK
>             y.color=BLACK
>             z.p.p.color=RED
>             z=z.p.p
>           else ifz==z.p.right
>                 z=z.p
>                 Left-Rotate(T,z)
>             z.p.color=BLACK
>             z.p.p.color=RED
>             RIGHT-ROTATE(T,z.p.p)
>       else(same as)
> T.root.color=BLACK
> ```
>
> 