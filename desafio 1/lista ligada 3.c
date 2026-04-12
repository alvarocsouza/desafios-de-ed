#include<stdio.h>

struct no {
    int info;         
    struct no * prox;
};

struct no * novoNo(int info) {
    struct no * novo = malloc(sizeof(struct no));
    novo->info = info;
    return novo;
}

struct no * insert_first(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    if (!novo) return lista;   // se falhar, mantém a lista como estava
    novo->prox = lista;        // novo aponta para a antiga cabeça
    return novo;               // novo vira a cabeça
}

void insert_last(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    // Precisamos encontrar o último elemento da lista. Quando encontramos, adicionamos o novo!
    struct no *curr = lista;
    while (curr->prox != NULL) {
        curr = curr->prox;
    }
    curr->prox = novo;
}

struct no * remove_first(struct no * lista) {
    if (lista == NULL) return NULL; // lista vazia

    struct no *novo_inicio = lista->prox;
    return novo_inicio;
}
void print_list(struct no *lista){

    struct no *curr;

    for(curr = lista ; curr != NULL ; curr = curr->prox){
        printf("%d => " , curr->info);
    }
    printf("NULL\n");
}

void remove_last(struct no * lista){

    struct no *curr;

    for(curr = lista ; curr->prox->prox != NULL ; curr = curr->prox);

    curr->prox = NULL;
}

void remove_value(struct no *lista, int value){

    struct no * curr;

    for(curr = lista ; curr->prox->info != value ; curr = curr->prox);
    curr->prox = curr->prox->prox;

}//passa pela lista usando um ponteiro "atual" e quando ele encontra o valor que precisa ser removido
// ele faz o curr->prox ser igual ao curr->prox->prox, fazendo a ordem dde ponteiros da lista pular o elemento que queremos remover



int main() {

    struct no * lista = NULL;

    for (int i = 10 ; i <= 100 ; i+=10)
        lista = insert_first(lista , i);
    
    print_list(lista);

    printf("\n LISTA SEM O 50:\n");

    remove_value(lista , 50);

    print_list(lista);
    
    return 0;
}