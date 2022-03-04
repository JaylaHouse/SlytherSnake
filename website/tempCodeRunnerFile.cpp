#include <iostream>

int bruh(int a, int n);

int main(){

    std::cout<<bruh(6,4);
}



int bruh(int a, int n){
    if(n==0){
        return 1;
    }
    else return a*bruh(a, n-1);
}