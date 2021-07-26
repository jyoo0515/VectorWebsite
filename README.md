# VectorWebsite

---

## Project Structure

#### 1) VectorWebsite.API

- API controller가 담긴 프로젝트
- Clinet 요청을 받고 [MediatR](https://medium.com/@ducmeit/net-core-using-cqrs-pattern-with-mediatr-part-1-55557e90931b)을 통해 Query나 Command를 Application 프로젝트로 전달
- Client에서 파일 업로드가 이루어질 경우 wwwroot/Uploads/{id}/ 에 저장됨

#### 2) VectorWebsite.Application

- 실질적으로 요청 처리가 이루어지는 곳
  - CRUD 작업에 필요한 로직이 정의되어 있음
- AutoMapper를 사용하여 DB에 저장되어 있는 모델을 DTO로 매핑해 전달
  - AutoMapper에 필요한 Profile 역시 정의되어 있음

#### 3) VectorWebsite.Domain

- 모델이 정의되어 있는 곳
- ApplicationUser 모델은 [IdentityUser](https://docs.microsoft.com/ko-kr/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1) 클래스를 상속받아 사용
  - 이 경우 singinMangner와 loginManager와 같은 기능을 사용 가능
- DTO(Data Transfer Object) 또한 정의되어 있음

#### VectorWebsite.Infrastructure

- 다양한 용도로 사용되는 Middleware가 정의되어 있는 곳
  - 예) JWT 방식에 사용할 Token Generator

#### VectorWebsite.Persistance

- [DBContext](https://docs.microsoft.com/ko-kr/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0)가 정의되어 있는곳
- DB Migration History 확인 가능

## Naming Convention

- 기본적으로 Pascal Case 사용
- private 변수는 underscore(\_)로 시작
- const 변수는 ALL UPPERCASE, 띄어쓰기는 underscore

## Git Flow

- main 브랜치는 제품으로 출시될 수 있는 브랜치
- dev 브랜치는 기능이 개발되는 브랜치
- 개인 브랜치를 만들어서 작업한 뒤에 dev로 pull request
- Review 후에 merge함
